using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutes.Controllers {
    public class HomeController : Controller {
        // GET: UrlsAndRoutes/Home
        public ActionResult Index() {

            ViewBag.Controller = "Home page";
            ViewBag.Action = "Index";
            return View("_ActionName");
        }
    }
}