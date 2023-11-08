using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YTP.Main.Models;

namespace YTP.Main.ViewModels {
    public class VM_MiniDisplay {
        public List<MiniDisplay> MiniDisplay { get; set; }
        public List<Module> Module { get; set; }
    }
}