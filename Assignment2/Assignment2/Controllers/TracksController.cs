using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();

        // GET: Tracks
        public ActionResult Index()
        {
            var t = m.TrackGetAll();

            return View(t);
        }

        public ActionResult JazzIndex()
        {
            var j = m.TrackGetAllJazz();

            return View("Index",j); //by mentioning "Index" inside the (), you don't need to make the view again as it will redirects to automatically.
        }

        public ActionResult RogerGloverIndex()
        {
            var rg = m.TrackGetAllRogerGlover();

            return View("Index", rg);
        }

        public ActionResult Top50LongestIndex()
        {
            var top50 = m.TrackGetAllTop50Longest();

            return View("Index", top50);
        }



        // GET: Tracks/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }        

        // GET: Tracks/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tracks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
