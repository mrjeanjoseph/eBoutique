using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace YTP.Main.Areas.SportsStore {
    public class SportsStoreAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "SportsStore";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {

            context.MapRoute(null, 
                "SportsStore",
                new { controller = "Product", action = "ListProducts", category = (string)null, page = 1 });

            context.MapRoute(null,
                "SportsStore/Page{page}",
                new { controller = "Product", action = "ListProducts", category = (string)null },
                new { page = @"\d+" });

            context.MapRoute(null,
                "SportsStore/{category}",
                new { controller = "Product", action = "ListProducts", page = 1 });

            context.MapRoute(null,
                "SportsStore/{category}/Page{page}",
                new { controller = "Product", action = "ListProducts" },
                new { page = @"\d+" });

            context.MapRoute(null,
                "SportsStore/{controller}/{action}",
                new { controller = "Cart", action = "Checkout", id = UrlParameter.Optional });

            context.MapRoute("SportsStore", "{controller}/{action}"); //Not entirely sure what this does.
        }

        //public override void RegisterArea(AreaRegistrationContext context) {
        //    context.MapRoute(
        //        null,
        //        url: "Page{page}",
        //        defaults: new { controller = "Product", action = "ListProducts" }
        //    );
        //    context.MapRoute(
        //        "SportsStore",
        //        "SportsStore/{controller}/{action}/{id}",
        //        new { action = "ListProducts", id = UrlParameter.Optional },
        //        new[] { GetType().Namespace + ".Controllers" }
        //    );
        //}
    }
}