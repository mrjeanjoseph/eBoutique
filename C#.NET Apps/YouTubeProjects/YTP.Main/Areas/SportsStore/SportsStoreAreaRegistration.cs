using System.Web.Mvc;

namespace YTP.Main.Areas.SportsStore
{
    public class SportsStoreAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SportsStore";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SportsStore",
                "SportsStore/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );
        }
    }
}