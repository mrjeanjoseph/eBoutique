using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutes {
    public class UrlsAndRoutesAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "UrlsAndRoutes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {


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

        }
    }
}