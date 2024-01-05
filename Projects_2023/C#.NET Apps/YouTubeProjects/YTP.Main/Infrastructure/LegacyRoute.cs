using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YTP.Main.Infrastructure {
    public class LegacyRoute {
        private readonly string[] _urls;

        public LegacyRoute(string[] targetUrls) {
            _urls = targetUrls;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext,
            RouteData result = null) { 

            string requestURL = httpContext.Request
                .AppRelativeCurrentExecutionFilePath;
            if (_urls.Contains(requestURL, StringComparer.OrdinalIgnoreCase)) {
                result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyURL");
                result.Values.Add("LegacyURL", "requestedURL");
            }

            return result;

        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, 
            RouteValueDictionary values) {
            
            return null;
        }
    }
}