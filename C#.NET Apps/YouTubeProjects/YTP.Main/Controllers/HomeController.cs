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

            var miniDisplay = new List<MiniDisplay>() {
                new MiniDisplay {
                    DisplayName = "This Year Pickup Truck", 
                    DisplayDescription = "This is detailed description of mini display one.",
                    DisplayImageLink = "https://images8.alphacoders.com/616/thumb-1920-616810.jpg"
                },
                new MiniDisplay {
                    DisplayName = "Just Another TTP", 
                    DisplayDescription = "This is detailed description of mini display Rep.",
                    DisplayImageLink = "https://images.alphacoders.com/644/644418.jpg"
                },
                new MiniDisplay {
                    DisplayName = "Mini Display - Rep", 
                    DisplayDescription = "This is detailed description of mini display Rep.",
                    DisplayImageLink = ""
                },
                new MiniDisplay {
                    DisplayName = "Mini Display Three", 
                    DisplayDescription = "This is detailed description of mini display Three."
                },
                new MiniDisplay {
                    DisplayName = "One of a kind trucks", 
                    DisplayDescription = "This is detailed description of cool looking truck.",
                    DisplayImageLink = "https://wallpapers.com/images/hd/ford-truck-tr8rsjeai7pbxeny.jpg"
                },

            };

            var module = new List<Module>() {
                new Module {ModuleName = "CRUD Main Page", ModuleDescription = "This is just another detail module description"}
            };

            var miniDisplayObj = new VM_MiniDisplay {
                MiniDisplay = miniDisplay,
                Module = module,
            };

            return View(miniDisplayObj);
        }

        public ActionResult UnderConstruction() {
            return View();
        }

    }
}