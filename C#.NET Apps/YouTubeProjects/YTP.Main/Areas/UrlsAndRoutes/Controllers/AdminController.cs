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

        public ActionResult CustomVariables(string id) { // We can also make it optional (string id = "DefaultId")
            ViewBag.Controller = "Admin page";
            ViewBag.Action = "CustomVariables Action Method";
            //ViewBag.CustomVariables = RouteData.Values["id"];
            //ViewBag.CustomVariables = id;
            ViewBag.CustomVariables = id ?? "<no value>";
            return View("CustomVariables");
        }
    }
}