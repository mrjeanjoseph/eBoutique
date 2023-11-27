using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutesTwo {
    public class UrlsAndRoutesTwoAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "UrlsAndRoutesTwo"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {

            context.MapRoute(
                "",
                "UrlsAndRoutesTwo/New/Link{action}",
                new {controller = "Admin" }
            );

            context.MapRoute(
                "UrlsAndRoutesTwo",
                "UrlsAndRoutesTwo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}