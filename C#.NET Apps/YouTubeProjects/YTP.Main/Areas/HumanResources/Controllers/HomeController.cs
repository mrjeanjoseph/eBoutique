using System.Web.Mvc;

namespace YTP.Main.Areas.HumanResources.Controllers {
    public class HomeController : Controller {
        // GET: HumanResources/Home
        public ActionResult Index() {
            return View();
        }
    }
}