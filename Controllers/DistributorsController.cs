using Antlr.Runtime.Misc;
using MoviesDBManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesDBManager.Controllers
{
    public class DistributorsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Distributors(bool forceRefresh = false)
        {
            if (forceRefresh || DB.Distributors.HasChanged)
            {
                return PartialView(DB.Distributors.ToList().OrderBy(c => c.Name));
            }
            return null;
        }
        public ActionResult Create()
        {
            return View(new Distributor());
        }

        [HttpPost]
        public ActionResult Create(Distributor distributor, List<int> SelectedMoviesId)
        {
            if (ModelState.IsValid)
            {
                DB.Distributors.Add(distributor);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            Distributor distributor = DB.Distributors.Get(id);
            if (distributor != null)
            {
                return View(distributor);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Distributor distributor = DB.Distributors.Get(id);
            if (distributor != null)
            {
                return View(distributor);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Distributor distributor, List<int> SelectedMovies)
        {
            if (ModelState.IsValid)
            {
                DB.Distributors.Update(distributor, SelectedMovies);
                return RedirectToAction("Index");
            }
            return View(distributor);
        }

        public ActionResult Delete(int id)
        {
            DB.Distributors.Delete(id);
            return RedirectToAction("Index");
        }
    }
}