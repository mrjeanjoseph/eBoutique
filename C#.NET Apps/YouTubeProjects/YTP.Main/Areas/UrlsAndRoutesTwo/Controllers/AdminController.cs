using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutesTwo.Controllers {
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

        public ViewResult MyActionMethod() {
            string myActionUrl = Url.Action("Index", new { id = "MyID" });
            string myRouteUrl = Url.RouteUrl("Index", new { controller = "Home", action = "Index" });
            //... Do something with URLS...
            return View();
        }

        public RedirectToRouteResult MyActionMethod2() {
            var routeOne = RedirectToAction("Index");
            var routeTwo = RedirectToRoute( new { controller = "Home", action = "Index", id = "MyID"});

            return routeOne ?? routeTwo;
        }

        public ActionResult GetLegacyURL(string legacyURL) {
            return View((object)legacyURL);
        }
    }
}