using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cv.Controllers
{
    public class CVController : Controller
    {
        // GET: CV
        public ActionResult home()
        {
            return View();
        }

        public ActionResult Education()
        {
            return View();
        }
        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult Refference()
        {
            return View();
        }
    }
}