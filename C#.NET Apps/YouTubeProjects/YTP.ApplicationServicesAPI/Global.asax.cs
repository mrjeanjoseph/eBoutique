using System.Web;
using System.Web.Http;

namespace YTP.CommonAPI {

    public class WebApiApplication : HttpApplication {

        protected void Application_Start() {

            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
    }
}
