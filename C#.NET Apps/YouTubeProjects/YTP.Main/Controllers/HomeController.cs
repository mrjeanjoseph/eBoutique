using System;
using System.Web.Mvc;
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

            return View(result);

        }

        [HttpGet]
        public ViewResult RSVPForm() {
            return View();
        }

        [HttpPost]
        public ViewResult RSVPForm(GuestResponse response) {
            // TODO: Email response to the pary organizer
            return View("Thanks",response);
        }

        public ActionResult SomeStylings() {

            string cssStyling = "background-color: yellow;";
            ViewBag.cssStyling = cssStyling;

            return View("Index");
        }

        public ActionResult UnderConstruction() {
            return View();
        }

    }
}