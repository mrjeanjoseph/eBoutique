using System.Web.Mvc;

namespace YTP.Main.Areas.OperatingManagement
{
    public class OperatingManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get { return "OperatingManagement"; }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OperatingManagement",
                "OperatingManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { GetType().Namespace + ".Controllers" }
            );
        }
    }
}