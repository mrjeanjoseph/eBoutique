using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.Ouest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Ouest/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}