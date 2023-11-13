using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YTP.Main.Infrastructure;

namespace YTP.Main {
    public class MvcApplication : HttpApplication {
        protected void Application_Start() {

            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

    }
}
