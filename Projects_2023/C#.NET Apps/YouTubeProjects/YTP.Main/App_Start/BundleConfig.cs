using System.Web.Optimization;

namespace YTP.Main {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            //All Global Styles and Scripts
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/GlobalAsset/styles/bootstrap.css",
                      "~/GlobalAsset/styles/icons/bootstrap-icons.css",
                      "~/GlobalAsset/styles/dataTables.bootstrap5.css",
                      "~/GlobalAsset/styles/themes/base/jquery-ui.min.css",
                      "~/GlobalAsset/styles/alertifyjs/themes/default.min.css",
                      "~/GlobalAsset/styles/alertifyjs/alertify.min.css",
                      "~/GlobalAsset/styles/main.site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/GlobalAsset/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                      "~/GlobalAsset/scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                      "~/GlobalAsset/scripts/jquery-ui-1.13.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                      "~/GlobalAsset/scripts/jquery.dataTables.min.js",
                      "~/GlobalAsset/scripts/dataTables.bootstrap5.min.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/GlobalAsset/scripts/bootstrap.bundle.js"));

            bundles.Add(new Bundle("~/bundles/globalscripts").Include(
                      "~/GlobalAsset/scripts/notify.min.js",
                      "~/GlobalAsset/scripts/alertify.min.js",
                      "~/GlobalAsset/scripts/main.site.js"));
            
            //All Local Styles and Scripts
            bundles.Add(new Bundle("~/bundles/localscripts").Include(
                      "~/Areas/HumanResources/LocalAssets/haitiemployee.js",
                      "~/Areas/HumanResources/LocalAssets/site.home.js"));

            bundles.Add(new Bundle("~/Content/localstyles").Include(
                      "~/Areas/SportsStore/LocalAsset/site.css"));
        }
    }
}
