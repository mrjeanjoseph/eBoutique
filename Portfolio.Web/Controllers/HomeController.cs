using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Portfolio.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            Server.TransferRequest(Request.Url.AbsolutePath, false);
            return View();
        }
    }
}