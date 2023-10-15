using System.Web;
using System.Web.Http;

namespace YTP.ApplicationServicesAPI {

    public class WebApiApplication : HttpApplication {

        protected void Application_Start() {

            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
    }
}
