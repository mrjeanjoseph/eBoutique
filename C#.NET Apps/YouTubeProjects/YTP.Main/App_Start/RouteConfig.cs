using System.Web.Mvc;
using System.Web.Routing;

namespace YTP.Main {

    public class RouteConfig {

        public static void RegisterRoutes(RouteCollection routes) {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("MyRoute", "{controller}/{action}",
                new { controller = "Home", action = "Index", });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
                new string[] { "YTP.Main.Controllers" }
            );
        }
    }
}