using System.Web.Mvc;

namespace YTP.Main.Areas.HumanResources {
    public class HumanResourcesAreaRegistration : AreaRegistration {
        public override string AreaName {
            get { return "HumanResources"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) {

            context.MapRoute(
                "HumanResources",
                "HumanResources/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );
        }
    }
}