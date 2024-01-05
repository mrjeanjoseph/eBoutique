using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class RazorSyntaxController : Controller {

        private string viewPath = ConfigurationManager.AppSettings["et_viewpath"];

        readonly Product razorProduct = new Product() {
            ProductID = 1,
            Name = "Bouyon Pye Bef",
            Description = "Bouyon as fet ak pye bef",
            Category = "Kizin Nasyonal",
            ProductPrice = 950M
        };

        // GET: SandboxTBP/RazorSyntax
        public ActionResult Index() {
            viewPath += "Index.cshtml";
            return View(viewPath, razorProduct);
        }

        public ActionResult DisplayNameAndPrice() {

            viewPath += (MethodBase.GetCurrentMethod().Name + ".cshtml");
            return View(viewPath, razorProduct);
        }

        public ActionResult DemoExpresssion() {

            ViewBag.ProductCount = 15;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;

            viewPath += (MethodBase.GetCurrentMethod().Name + ".cshtml");
            return View(viewPath, razorProduct);

        }

        public ActionResult DemoArray() {

            Product[] products = {
                new Product {Name = "Bouk Kabrit", ProductPrice = 1125.51M},
                new Product {Name = "Kalbas", ProductPrice = 15.18M},
                new Product {Name = "Bouret", ProductPrice = 22.10M},
                new Product {Name = "Mouton", ProductPrice = 925.01M},
                new Product {Name = "Cheval", ProductPrice = 2825.00M}
            };

            viewPath += (MethodBase.GetCurrentMethod().Name + ".cshtml");
            return View(viewPath, products);
        }
    }
}