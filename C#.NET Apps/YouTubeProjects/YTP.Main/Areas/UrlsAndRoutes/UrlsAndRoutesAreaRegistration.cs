using System.Web.Mvc;

namespace YTP.Main.Areas.UrlsAndRoutes
{
    public class UrlsAndRoutesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UrlsAndRoutes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UrlsAndRoutes",
                "UrlsAndRoutes/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );
        }
    }
}