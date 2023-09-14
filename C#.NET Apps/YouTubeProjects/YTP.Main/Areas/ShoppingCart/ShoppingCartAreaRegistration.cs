using System.Web.Mvc;

namespace YTP.Main.Areas.ShoppingCart {
    public class ShoppingCartAreaRegistration : AreaRegistration {

        public override string AreaName {

            get { return "ShoppingCart"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {

            context.MapRoute(
                "ShoppingCart",
                "ShoppingCart/{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "YTP.Main.Areas.ShoppingCart.Controllers" }
            );
        }
    }
}