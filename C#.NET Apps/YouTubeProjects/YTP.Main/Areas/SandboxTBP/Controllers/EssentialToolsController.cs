using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.Areas.SandboxTBP.Models;
using YTP.Main.DataAccess;

namespace YTP.Main.Areas.SandboxTBP.Controllers {

    public class EssentialToolsController : Controller {

        private readonly ETProduct[] products = {
            new ETProduct { ProductName = "Bef Gras", Category = "Elvaj", ProductPrice = 2100M},
            new ETProduct { ProductName = "Bouk Kabrit", Category = "Elvaj", ProductPrice = 516M},
            new ETProduct { ProductName = "Cheval Noir", Category = "Elvaj", ProductPrice = 2850M},
            new ETProduct { ProductName = "Bari Pistash", Category = "Rekot", ProductPrice = 1150M},
            new ETProduct { ProductName = "Ke Palmis", Category = "Natif", ProductPrice = 472M}
        };

        // GET: SandboxTBP/EssentialTools
        public ActionResult Index() {

            IValueCalculator calc = new LinqValueCalculator();
            ETShoppingCart cart = new ETShoppingCart(calc) { Products = products };

            decimal totalValue = cart.CalculatorProductTotal();

            return View(totalValue);
        }
    }
}