using System.Web.Mvc;

namespace YTP.Main.Areas.VehicleRentalSystem
{
    public class VehicleRentalSystemAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "VehicleRentalSystem";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "VehicleRentalSystem_default",
                "VehicleRentalSystem/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}