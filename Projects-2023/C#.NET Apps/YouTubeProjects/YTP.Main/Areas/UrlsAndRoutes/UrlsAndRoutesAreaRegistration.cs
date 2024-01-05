using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using YTP.Main.App_Start;

namespace YTP.Main.Areas.UrlsAndRoutes {
    public class UrlsAndRoutesAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "UrlsAndRoutes"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {

            //// context.MapMvcAttributeRoutes(); // Does not work!

            context.MapRoute("",
                "UrlsAndRoutes/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UrlsAndRoutes.ControllersAlt" });

            // Constraining Routes
            context.MapRoute("ChromeRoute", "{*catchall}",
                new { controller = "Home", action = "Index" },
                new { customConstraint = new UserAgentConstraint("chrome") },
                new[] { "UrlsAndRoutes.ControllersAlt" }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new {
                    controller = "^H.*", action = "^Index$|^About$",
                    HttpMethod = new HttpMethodConstraint("GET"),
                    //HttpMethod = new HttpMethodConstraint("GET", "POST"), // We can also limit post as well.
                    //id = new RangeRouteConstraint(10, 20),
                    id = new CompoundRouteConstraint(new IRouteConstraint[] {
                        new AlphaRouteConstraint(),
                        new MinLengthRouteConstraint(6),
                    }), // Don't know what the hell is happening
                },
                new[] { GetType().Namespace + ".Controllers", }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new {
                    controller = "^hg.*",
                    action = "^Index$|^About$",
                    HttpMethod = new HttpMethodConstraint("GET")
                },
                new[] { GetType().Namespace + ".Controllers", }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.*", action = "^Index$|^About$" }, //Creating precise routes
                new[] { GetType().Namespace + ".Controllers", }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.*" },
                new[] { GetType().Namespace + ".Controllers", }
            );
            //=============================================================================

            //Prioritizing Controllers by Namespaces
            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UrlsAndRoutes.ControllersAlt", GetType().Namespace + ".Controllers", }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UrlsAndRoutes.ControllersAlt", "UrlsAndRoutes.Controllers" }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UrlsAndRoutes.ControllersAlt" }
            );
            //=============================================================================

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}/{*catchall}",
                new { controller = "Admin", action = "CustomVariables", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}",
                new { controller = "Admin", action = "CustomVariables", id = "DefaultId" },
                new[] { GetType().Namespace + ".Controllers" }
            );

            context.MapRoute(
                "",
                "AreaName/ControllerName/{action}/{id}", // We can rename all the segment so long as we provide controller and index name
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/mixing1{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes222/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );

            context.MapRoute(
                "",
                "UrlsAndRoutes/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );

            Route myRoute = context.MapRoute("AddControllerRoute",
                "Home/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UrlsAndRoutes.ControllersAlt" }
                );
            myRoute.DataTokens["UseNamespaceFallback"] = false;

        }
    }
}