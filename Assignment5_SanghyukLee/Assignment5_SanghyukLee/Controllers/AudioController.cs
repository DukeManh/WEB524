using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5_SanghyukLee.Controllers
{
    public class AudioController : Controller
    {
        Manager m = new Manager();

        // GET: Audio
        public ActionResult Index()
        {
            return View();
        }

        // GET: Audio/Details/5
        [Route("audio/{id}")]
        public ActionResult Details(int? id)
        {
            var a = m.TrackAudioGetById(id.GetValueOrDefault());

            if (a == null)
            {
                return HttpNotFound();
            }
            else
            {

                if (a.AudioContentType != null)
                {
                    return File(a.Audio, a.AudioContentType);
                }

                return Content("No audio");
            }

        }


    }
}
