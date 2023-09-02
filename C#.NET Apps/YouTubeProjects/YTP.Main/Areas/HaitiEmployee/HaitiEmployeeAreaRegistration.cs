using System.Web.Mvc;

namespace YTP.Main.Areas.HaitiEmployee
{
    public class HaitiEmployeeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get{return "HaitiEmployee";}
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HaitiEmployee_default",
                "HaitiEmployee/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}