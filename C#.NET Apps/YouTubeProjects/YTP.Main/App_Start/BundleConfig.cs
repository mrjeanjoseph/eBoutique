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
                      "~/GlobalAssets/ScriptFiles/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/GlobalAssets/ScriptFiles/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                      "~/GlobalAssets/ScriptFiles/jquery-ui-1.13.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                      "~/GlobalAssets/ScriptFiles/jquery.dataTables.min.js",
                      "~/GlobalAssets/ScriptFiles/dataTables.bootstrap5.min.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/GlobalAssets/ScriptFiles/bootstrap.bundle.js"));

            bundles.Add(new Bundle("~/bundles/globalscripts").Include(
                      "~/GlobalAssets/ScriptFiles/notify.min.js",
                      "~/GlobalAssets/ScriptFiles/alertify.min.js",
                      "~/GlobalAssets/ScriptFiles/main.site.js"));
            
            //All Local Styles and Scripts
            bundles.Add(new Bundle("~/bundles/localscripts").Include(
                      "~/Areas/HumanResources/LocalAssets/haitiemployee.js",
                      "~/Areas/HumanResources/LocalAssets/site.home.js"));
        }
    }
}
