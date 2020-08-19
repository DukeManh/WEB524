using Assignment5_SanghyukLee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5_SanghyukLee.Controllers
{
    public class AlbumsController : Controller
    {
        Manager m = new Manager();

        // GET: Albums
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        [Authorize(Roles = "Executive, Coordinator, Clerk, Staff")]
        public ActionResult Details(int? id)
        {
            var obj = m.AlbumGetById(id.GetValueOrDefault());
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(obj);
            }

        }

        //GET
        [Authorize(Roles = "Clerk"), Route("albums/{id}/addtrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());

            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                var trackAdd = new TrackAddFormViewModel();
                trackAdd.AlbumId = album.AlbumId;
                trackAdd.AlbumName = album.Name;
                trackAdd.TrackGenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                return View(trackAdd);
            }
        }

        //POST
        [Authorize(Roles = "Clerk"), Route("albums/{id}/addtrack")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTrack(TrackAddViewModel track)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(track);
                }

                // TODO: Add insert logic here
                var addedTrack = m.TrackAdd(track);

                if (addedTrack == null)
                {
                    return View(track);
                }
                else
                {
                    return RedirectToAction("details", "tracks", new { id = addedTrack.TrackId });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
