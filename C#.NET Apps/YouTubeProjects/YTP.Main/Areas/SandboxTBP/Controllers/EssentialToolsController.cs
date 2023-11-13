﻿using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class EssentialToolsController : Controller {

        private string viewPath = ConfigurationManager.AppSettings["et_viewpath"];

        private readonly ET_Product[] products = {
            new ET_Product {ProductName = "Ble ak Pwa Kongo", Category = "Manje Peyi", UnitPrice = 231.5M},
            new ET_Product {ProductName = "Fritay Pwasson", Category = "Manje Peyi", UnitPrice = 78.1M},
            new ET_Product {ProductName = "Barik Pistash", Category = "Rekot", UnitPrice = 7459.3M},
            new ET_Product {ProductName = "Zaboka Blende ak let", Category = "Manje Peyi", UnitPrice = 63.2M},
            new ET_Product {ProductName = "Cheval", Category = "Rekot", UnitPrice = 2231.5M},
        };

        // GET: SandboxTBP/EssentialTools
        public ActionResult Index() {
            IValueCalculator calc = new LinqValueCalculator();

            ET_ShoppingCart cart = new ET_ShoppingCart(calc) { Products = products };

            decimal totalValue = cart.CalculateProductTotal();

            viewPath += (MethodBase.GetCurrentMethod().Name + ".cshtml");
            return View(viewPath, totalValue);
        }
    }
}