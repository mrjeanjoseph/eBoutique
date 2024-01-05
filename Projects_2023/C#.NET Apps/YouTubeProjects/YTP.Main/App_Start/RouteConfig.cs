using System.Web.Mvc;
using System.Web.Routing;

namespace YTP.Main {

    public class RouteConfig {

        public static void RegisterRoutes(RouteCollection routes) {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute("StaticMixing", "Mixing/{action}", 
                new { controller = "Home", action = "Index", },
                new string[] { "YTP.Main.Controllers" });

            routes.MapRoute("", "static2{controller}/{action}", // Looks like it needs action segments
                new string[] { "YTP.Main.Controllers" });

            routes.MapRoute("", "static1{controller}/{action}", 
                new { controller = "Home", action = "Index", },
                new string[] { "YTP.Main.Controllers" });

            routes.MapRoute("MyRoute", "z{controller}/{action}",// We can do static url segments - Notice the z
                new { controller = "Home", action = "Index", },
                new string[] { "YTP.Main.Controllers" });

            routes.MapRoute("", "{controller}/{action}",
                new { controller = "Home", action = "Index", },
                new string[] { "YTP.Main.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "YTP.Main.Controllers" }
            );
        }
    }
}