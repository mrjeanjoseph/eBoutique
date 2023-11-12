using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class RazorSyntaxController : Controller {

        private readonly string viewPath = ConfigurationManager.AppSettings["rs_viewpath"];

        readonly Product razorProduct = new Product() {
            ProductID = 1,
            Name = "Bouyon Pye Bef",
            Description = "Bouyon as fet ak pye bef",
            Category = "Kizin Nasyonal",
            ProductPrice = 950M
        };

        // GET: SandboxTBP/RazorSyntax
        public ActionResult Index() {
            return View(viewPath, razorProduct);
        }
    }
}