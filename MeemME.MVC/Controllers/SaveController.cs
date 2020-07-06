using MeemME.Models.SaveModels;
using MeemME.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeemME.MVC.Controllers
{
    public class SaveController : Controller
    {
        private SaveService CreateSaveService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SaveService(userId);
            return service;
        }

        // GET: Note
        public ActionResult Index()
        {
            var service = CreateSaveService();
            var model = service.GetSaves();

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
        public ActionResult Create(SaveCreate model)
        {
            var service = CreateSaveService();

            if (service.CreateSave(model))
            {
                TempData["SaveResult"] = "Your epic meme was saved!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "the meme could not be saved.");

            return View(model);
        }

        //GET Details
        public ActionResult Details(int id)
        {
            var svc = CreateSaveService();
            var model = svc.GetSaveById(id);

            return View(model);
        }

        //GET Edit
        public ActionResult Edit(int id)
        {
            var service = CreateSaveService();
            var detail = service.GetSaveById(id);
            var model =
                new SaveEdit
                {
                    SaveID = detail.SaveID,
                    Title = detail.Title,
                    URL = detail.URL
                };
            return View(model);
        }

        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SaveEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SaveID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateSaveService();

            if (service.UpdateSave(model))
            {
                TempData["SaveResult"] = "Your saved meme was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your saved meme could not be updated. The system requires that you make an edit to your Meme.");
            return View(model);
        }
        //GET Delete
        public ActionResult Delete(int id)
        {
            var svc = CreateSaveService();
            var model = svc.GetSaveById(id);

            return View(model);
        }
        //POST Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSaveService();

            service.DeleteSave(id);

            TempData["SaveResult"] = "Your meme was deleted";

            return RedirectToAction("Index");
        }
    }
}