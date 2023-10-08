using System.Web.Optimization;

namespace YTP.Main {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            //All Global Styles and Scripts
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/GlobalAssets/styles/bootstrap.css",
                      "~/GlobalAssets/styles/dataTables.bootstrap5.css",
                      "~/GlobalAssets/styles/themes/base/jquery-ui.min.css",
                      "~/GlobalAssets/styles/alertifyjs/themes/default.min.css",
                      "~/GlobalAssets/styles/alertifyjs/alertify.min.css",
                      "~/GlobalAssets/styles/main.site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/GlobalAssets/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/GlobalAssets/scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                      "~/GlobalAssets/scripts/jquery-ui-1.13.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                      "~/GlobalAssets/scripts/jquery.dataTables.min.js",
                      "~/GlobalAssets/scripts/dataTables.bootstrap5.min.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/GlobalAssets/scripts/bootstrap.bundle.js"));

            bundles.Add(new Bundle("~/bundles/globalscripts").Include(
                      "~/GlobalAssets/scripts/notify.min.js",
                      "~/GlobalAssets/scripts/alertify.min.js",
                      "~/GlobalAssets/scripts/main.site.js"));
            
            //All Local Styles and Scripts
            bundles.Add(new Bundle("~/bundles/localscripts").Include(
                      "~/Areas/HumanResources/LocalAssets/haitiemployee.js",
                      "~/Areas/HumanResources/LocalAssets/site.home.js"));
        }
    }
}
