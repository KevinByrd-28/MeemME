using MeemME.Data;
using MeemME.Models.ImageModels;
using MeemME.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeemME.MVC.Controllers
{
    public class ImageController : Controller
    {
        private ImageService CreateImageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ImageService(userId);
            return service;
        }

        // GET: Note
        public ActionResult Index()
        {
            var service = CreateImageService();
            var model = service.GetImages();

            return View(model);
        }

        //GET:Note
        public ActionResult Create()
        {
            return View();
        }

        //POST: Note
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageCreate model)
        {

            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            model.ImagePath = "~/Photos/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Photos/"), fileName);
            model.ImageFile.SaveAs(fileName);
            if (!ModelState.IsValid) return View(model);

            var service = CreateImageService();

            if (service.CreateImage(model))
            {
                TempData["SaveResult"] = "Your meme was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "the meme could not be created.");

            return View(model);
        }

        //GET Details
        public ActionResult Details(int id)
        {
            var svc = CreateImageService();
            var model = svc.GetImageById(id);

            return View(model);
        }

        //GET Edit
        public ActionResult Edit(int id)
        {
            var service = CreateImageService();
            var detail = service.GetImageById(id);
            var model =
                new ImageEdit
                {
                    ImageID = detail.ImageID,
                    Title = detail.Title,
                    ImagePath = detail.ImagePath,
                    ImageFile = detail.ImageFile,
                    TopText = detail.TopText,
                    BottomText = detail.BottomText,
                };
            return View(model);
        }

        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ImageEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ImageID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateImageService();

            if (service.UpdateImage(model))
            {
                TempData["SaveResult"] = "Your meme was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your meme could not be updated. The system requires that you make an edit to your Meme.");
            return View(model);
        }
        //GET Delete
        public ActionResult Delete(int id)
        {
            var svc = CreateImageService();
            var model = svc.GetImageById(id);

            return View(model);
        }
        //POST Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateImageService();

            service.DeleteImage(id);

            TempData["SaveResult"] = "Your meme was deleted";

            return RedirectToAction("Index");
        }




        
    }
}