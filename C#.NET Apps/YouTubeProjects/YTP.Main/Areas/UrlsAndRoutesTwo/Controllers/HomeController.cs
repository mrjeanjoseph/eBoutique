using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutesTwo.Controllers {
    public class HomeController : Controller {
        // GET: UrlsAndRoutes/Home

        public ActionResult Index() {

            ViewBag.Controller = "Home page";
            ViewBag.Action = "Index";
            return View("_ActionName");
        }
    }
}