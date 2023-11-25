using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutes.Controllers
{
    public class CustomerController : Controller
    {
        // GET: UrlsAndRoutes/Customer
        public ActionResult Index() {
            ViewBag.Controller = "Customer page";
            ViewBag.Action = "Index";
            return View("_ActionName");
        }
    }
}