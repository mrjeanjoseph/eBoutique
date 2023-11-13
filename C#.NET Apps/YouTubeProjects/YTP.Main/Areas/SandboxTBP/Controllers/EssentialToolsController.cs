using Ninject;
using System.Configuration;
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

        private readonly IValueCalculator _calc;
        public EssentialToolsController(IValueCalculator calcParam) {
            _calc = calcParam;
        }

        // Seperating the DI logic outside the controller
        public ActionResult Index() {
            ET_ShoppingCart cart = new ET_ShoppingCart(_calc) { Products = products };

            decimal totalValue = cart.CalculateProductTotal();

            viewPath += (MethodBase.GetCurrentMethod().Name + ".cshtml");
            return View(viewPath, totalValue);
        }

        // Setting the DI right inside the controller
        public ActionResult Index_LocalDI() {

            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
            IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();

            ET_ShoppingCart cart = new ET_ShoppingCart(calc) { Products = products };

            decimal totalValue = cart.CalculateProductTotal();

            viewPath += (MethodBase.GetCurrentMethod().Name + ".cshtml");
            return View(viewPath, totalValue);
        }

        // GET: SandboxTBP/EssentialTools
        public ActionResult Index_Old() { 
            IValueCalculator calc = new LinqValueCalculator();

            ET_ShoppingCart cart = new ET_ShoppingCart(calc) { Products = products };

            decimal totalValue = cart.CalculateProductTotal();

            viewPath += (MethodBase.GetCurrentMethod().Name + ".cshtml");
            return View(viewPath, totalValue);
        }


    }
}