using System.Web.Mvc;

namespace YTP.Main.Areas.RoySecHR
{
    public class RoySecHRAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RoySecHR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RoySecHR_default",
                "RoySecHR/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}