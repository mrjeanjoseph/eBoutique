using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutesTwo.Controllers {
    public class CustomerController : Controller {

        [Route("~/CustTest2")]
        public ActionResult Index() {
            ViewBag.Controller = "Customer page";
            ViewBag.Action = "Index";
            return View("_ActionName");
        }
    }
}