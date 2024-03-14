using MoviesDBManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesDBManager.Controllers
{
    public class MoviesController : Controller
    {
        [OnlineUsers.UserAccess]
        public ActionResult Index()
        {
            return View();
        }
        [OnlineUsers.UserAccess]
        public ActionResult Movies(bool forceRefresh = false)
        {
            if (forceRefresh || DB.Movies.HasChanged)
            {
                return PartialView(DB.Movies.ToList().OrderBy(c => c.Name));
            }
            return null;
        }
        [OnlineUsers.PowerUserAccess]
        public ActionResult Create()
        {
            return View(new Movie());
        }

        [HttpPost]
        public ActionResult Create(Movie movie, List<int> SelectedActors, List<int> SelectedDistributorsId)
        {
            if (ModelState.IsValid)
            {
                DB.Movies.Add(movie);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            Movie movie = DB.Movies.Get(id);
            if (movie != null)
            {
                return View(movie);
            }
            return RedirectToAction("Index");
        }

        [OnlineUsers.PowerUserAccess]
        public ActionResult Edit(int id)
        {
            Movie movie = DB.Movies.Get(id);
            if (movie != null)
            {
               return View(movie);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Movie movie, List<int> SelectedActors, List<int> SelectedDistributors)
        {
            if (ModelState.IsValid)
            {
                DB.Movies.Update(movie, SelectedActors, SelectedDistributors);
                return RedirectToAction("Details", new { id = movie.Id });
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            DB.Movies.Delete(id);
            return RedirectToAction("Index");
        }
    }
}