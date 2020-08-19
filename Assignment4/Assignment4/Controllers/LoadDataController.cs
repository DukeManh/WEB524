using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment4.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        public ActionResult LoadData()
        {
            if (m.LoadData())
            {
                
                return Content("role data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        [Authorize(Roles = "Executive")]
        public ActionResult LoadDataArtist()
        {
            if (m.LoadDataArtist())
            {
                return Content("artist data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        [Authorize(Roles = "Coordinator")]
        public ActionResult LoadDataAlbum()
        {
            if (m.LoadDataAlbum())
            {
                return Content("album data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        [Authorize(Roles = "Clerk")]
        public ActionResult LoadDataTrack()
        {
            if (m.LoadDataTrack())
            {
                return Content("track data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        public ActionResult LoadDataGenre()
        {
            if (m.LoadDataGenre())
            {
                return Content("genre data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

    }
}