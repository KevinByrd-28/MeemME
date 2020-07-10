using MeemME.Data;
using MeemME.Models.FolderModels;
using MeemME.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeemME.MVC.Controllers
{
    public class FolderController : Controller
    {
        private FolderService CreateFolderService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FolderService(userId);
            return service;
        }

        private ImageService CreateImageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ImageService(userId);
            return service;
        }

        // GET: Folders
        public ActionResult Index()
        {
            var service = CreateFolderService();
            var model = service.GetFolders();

            return View(model);
        }

        //GET:Create
        public ActionResult Create()
        {
            var imgSer = CreateImageService();
            var getImage = imgSer.GetImages();
            ViewBag.Images = getImage.ToList();
            return View();
        }

        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FolderCreate model)
        {

            if (!ModelState.IsValid) return View(model);

            var service = CreateFolderService();

            if (service.CreateFolder(model))
            {
                TempData["SaveResult"] = "Your folder was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "the folder could not be created.");

            return View(model);
        }

        //GET Details
        public ActionResult Details(int id)
        {
            var svc = CreateFolderService();
            var model = svc.GetFolderById(id);

            return View(model);
        }

        //GET Edit
        public ActionResult Edit(int id)
        {
            var imgSer = CreateImageService();
            var getImage = imgSer.GetImages();
            ViewBag.Images = getImage.ToList();

            var service = CreateFolderService();
            var detail = service.GetFolderById(id);
            var model =
                new FolderEdit
                {
                    FolderID = detail.FolderID,
                    Name = detail.Name,
                    //ImagesInFolder = detail.ImagesInFolder,

                };
            return View(model);
        }

        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FolderEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.FolderID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateFolderService();

            if (service.UpdateFolder(model))
            {
                TempData["SaveResult"] = "Your folder was updated.";
                return RedirectToAction("Index");
            }

            //ModelState.AddModelError("", "An Error Occured.");
            //return View(model);
            return RedirectToAction("Index");
        }
        //GET Delete
        public ActionResult Delete(int id)
        {
            var svc = CreateFolderService();
            var model = svc.GetFolderById(id);

            return View(model);
        }
        //POST Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateFolderService();

            service.DeleteFolder(id);

            TempData["SaveResult"] = "Your folder was deleted";

            return RedirectToAction("Index");
        }
    }
}