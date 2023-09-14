using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.HumanResources.Controllers
{
    public class HomeController : Controller
    {
        // GET: HumanResources/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}