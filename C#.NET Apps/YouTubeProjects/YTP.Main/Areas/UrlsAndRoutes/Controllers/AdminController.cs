using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutes.Controllers {
    public class AdminController : Controller {
        // GET: UrlsAndRoutes/Admin
        public ActionResult Index() {
            ViewBag.Controller = "Admin page";
            ViewBag.Action = "Index";
            return View("_ActionName");
        }

        public ActionResult CustomVariables() {
            ViewBag.Controller = "Admin page";
            ViewBag.Action = "Custom Variables";
            ViewBag.CustomVariables = RouteData.Values["id"];
            return View("_ActionName");
        }
    }
}