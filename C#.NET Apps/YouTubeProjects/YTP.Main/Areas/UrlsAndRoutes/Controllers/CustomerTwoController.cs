using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutes.Controllers {

    [RoutePrefix("UsersTwo")]
    public class CustomerTwoController : Controller {
        // GET: UrlsAndRoutes/CustomerTwo

        [Route("~/TestTwo")]
        public ActionResult Index() {
            ViewBag.Controller = "Customer page Two";
            ViewBag.Action = "Index";
            return View("_ActionName");
        }

        [Route("Add/{user}/{id:int}")]
        public string Create(string user, int id) {
            return string.Format("User: {0}, ID: {1}", user, id);
        }

        [Route("Add/{user}/{password}")] //The route will differentiate between the string and the int
        public string ChangePass(string user, string password) {
            return string.Format("Change Password: User: {0}, Password: {1}", user, password);
        }
        public ActionResult List() {
            ViewBag.Controller = "The List page";
            ViewBag.Action = "List Action Method";
            return View("_ActionName");
        }
    }
}