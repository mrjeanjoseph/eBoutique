using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Infrastructure.Binders;

namespace YTP.Main {
    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
