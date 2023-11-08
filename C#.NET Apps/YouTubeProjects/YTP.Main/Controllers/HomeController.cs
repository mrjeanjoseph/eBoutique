using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.Models;
using YTP.Main.ViewModels;

namespace YTP.Main.Controllers {
    public class HomeController : Controller {
        // GET: Home
        //private readonly VM_MiniDisplay _md = new VM_MiniDisplay();

        public ActionResult Index(VM_MiniDisplay viewModel) {

            var result = viewModel.Something();

            return View(result);

        }

        public ActionResult UnderConstruction() {
            return View();
        }

    }
}