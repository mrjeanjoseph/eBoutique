using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YTP.Main.Models;

namespace YTP.Main.ViewModels {
    public class ModuleViewModel {
        public List<Module> Modules { get; set; }
        public List<Page> Pages { get; set; }
    }
}