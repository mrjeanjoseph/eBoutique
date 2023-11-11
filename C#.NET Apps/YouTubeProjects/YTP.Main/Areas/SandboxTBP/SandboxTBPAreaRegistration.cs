using System.Web.Mvc;

namespace YTP.Main.Areas.SandboxTBP
{
    public class SandboxTBPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SandboxTBP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SandboxTBP_default",
                "SandboxTBP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}