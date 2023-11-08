using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YTP.Main.Models;

namespace YTP.Main.ViewModels {
    public class VM_MiniDisplay {

        public List<MiniDisplay> MiniDisplay { get; set; }
        public List<Module> Module { get; set; }

        public Module ModuleOne { get; set; }

        public VM_MiniDisplay() {
            MiniDisplay = new List<MiniDisplay>();
        }


        public VM_MiniDisplay GenerateMiniDisplay() {

            var miniDisplay = new List<MiniDisplay>() {
                new MiniDisplay {
                    DisplayName = "This Year Pickup Truck2222",
                    DisplayDescription = "This is detailed description of mini display one.",
                    DisplayImageLink = "https://images8.alphacoders.com/616/thumb-1920-616810.jpg"
                },
                new MiniDisplay {
                    DisplayName = "Just Another TTP-222",
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

            var module_one = new Module() {
                ModuleName = "CRUD Main Page", ModuleDescription = "This is just another detail module description"
            };

            var miniDisplayObj = new VM_MiniDisplay {
                MiniDisplay = miniDisplay,
                Module = module,
                ModuleOne = module_one
            };


            return miniDisplayObj;
        }
    }
}