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
        public ActionResult Index() {

            var Module = new List<MiniDisplay>() {
                new MiniDisplay {ModuleName = "Module one", ModuleDescription = "This is detailed description of module one."},
                new MiniDisplay {ModuleName = "Module Two - Rep", ModuleDescription = "This is detailed description of module Rep."},
                new MiniDisplay {ModuleName = "Module Three", ModuleDescription = "This is detailed description of module Three."},
                new MiniDisplay {ModuleName = "Module ThreeB", ModuleDescription = "This is detailed description of module ThreeB."},
            };

            var Page = new List<Module>() {
                new Module {PageName = "This is a page", PageDescription = "This is just another detail page description"}
            };

            var ModuleViewModel = new ModuleViewModel {
                Modules = Module,
                Pages = Page,
            };

            return View(ModuleViewModel);
        }

        public ActionResult UnderConstruction() {
            return View();
        }

    }
}