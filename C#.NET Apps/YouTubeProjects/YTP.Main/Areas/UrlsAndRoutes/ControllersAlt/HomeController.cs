using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutes.ControllersAlt {
    public class HomeController : Controller {
        // GET: UrlsAndRoutes/HomeAlt
        public ActionResult Index() {

            ViewBag.Controller = "Home page - Alt";
            ViewBag.Action = "Index";
            return View("_ActionName");
        }
    }
}