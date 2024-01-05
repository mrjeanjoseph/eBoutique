using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutes.Controllers {
    public class CustomerController : Controller {
        // GET: UrlsAndRoutes/Customer
        public ActionResult Index() {
            ViewBag.Controller = "Customer page";
            ViewBag.Action = "Index";
            return View("_ActionName");
        }

        [Route("Users/Add/{user}/{id}")]
        public string Create(string user, int id) {
            return string.Format("User: {0}, ID: {1}", user, id);
        }

        [Route("Users/Add/{user}/{id:int}")] //The route will differentiate between the int and the string
        public string Create2(string user, int id) {
            return string.Format("The Create Method \n \tUser: {0}, ID: {1}", user, id);
        }

        [Route("Users/Add/{user}/{password}")] //The route will differentiate between the string and the int
        public string ChangePass(string user, string password) {
            return string.Format("Change Password: User: {0}, Password: {1}", user, password);
        }

        [Route("Users/Add/{user}/{password:alpha:length(6)}")] //combining constraints
        public string ChangePassWithConstraint(string user, string password) {
            return string.Format("Change Password: User: {0}, Password: {1}", user, password);
        }

        public ActionResult List() {
            ViewBag.Controller = "Customer page";
            ViewBag.Action = "List Action Method";
            return View("_ActionName");
        }
    }
}