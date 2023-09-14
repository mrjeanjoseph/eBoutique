using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        // GET: ShoppingCart/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}