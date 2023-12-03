
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using YTP.Main.Models;
using YTP.Main.ViewModels;

namespace YTP.Main.Controllers {
    public class HomeController : Controller {
        // GET: Home
        //private readonly VM_MiniDisplay _md = new VM_MiniDisplay();

        public ActionResult Index(VM_MiniDisplay viewModel) {

            var result = viewModel.GenerateMiniDisplay();
            int hour = DateTime.Now.Hour;
            string message;


            if (hour < 12) message = "Good morning";
            else if (hour > 12 && hour < 16) message = "Good Afternoon";
            else message = "Good Evening";

            ViewBag.Greeting = message;

            ViewBag.MessageTwo = MyActionMethod();

            return View(result);

        }

        [HttpGet]
        public ViewResult RSVPForm() {
            return View();
        }

        [HttpPost]
        public ViewResult RSVPForm(GuestResponse response) {

            if (ModelState.IsValid) {
                // TODO: Email response to the pary organizer
                ViewBag.username = ConfigurationManager.AppSettings["username"];
                ViewBag.password = ConfigurationManager.AppSettings["password"];

                return View("Thanks", response);
            }else {
                //There are validation error
                return View();
            }
        }

        public ActionResult SomeStylings() {

            string cssStyling = "background-color: yellow;";
            ViewBag.cssStyling = cssStyling;

            return View("Index");
        }

        public ActionResult UnderConstruction() {
            return View();
        }

        public string MyActionMethod() {
            string MyActionUrl = Url.Action("Index", new { id = "myID"});
            string MyRouteUrl = Url.RouteUrl( new { controller = "Home", action = "Index"});
            //...Do something with URLs...
            return ($" {MyActionUrl}, - {MyRouteUrl}");
        }

    }
}