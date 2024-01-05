using System.Web.Mvc;
using YTP.Main.Areas.SandboxTBP.Models;

namespace YTP.Main.Areas.SandboxTBP.Controllers {

    public class EssentialToolsController : Controller {

        private readonly IValueCalculator _calc;

        private readonly ETProduct[] products = {
            new ETProduct { ProductName = "Bef Gras", Category = "Elvaj", ProductPrice = 2100M},
            new ETProduct { ProductName = "Bouk Kabrit", Category = "Elvaj", ProductPrice = 516M},
            new ETProduct { ProductName = "Cheval Noir", Category = "Elvaj", ProductPrice = 2850M},
            new ETProduct { ProductName = "Bari Pistash", Category = "Rekot", ProductPrice = 1150M},
            new ETProduct { ProductName = "Ke Palmis", Category = "Natif", ProductPrice = 472M}
        };

        public EssentialToolsController(IValueCalculator calcParam, IValueCalculator calcParamTwo) {
            _calc = calcParam;
        }

        // GET: SandboxTBP/EssentialTools
        public ActionResult Index() {

            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();

            //IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();

            ETShoppingCart cart = new ETShoppingCart(_calc) { Products = products };

            decimal totalValue = cart.CalculatorProductTotal();

            return View(totalValue);
        }
    }
}