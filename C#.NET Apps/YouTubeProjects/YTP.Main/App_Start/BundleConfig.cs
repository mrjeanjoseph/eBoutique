using System.Web.Optimization;

namespace YTP.Main {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            //All Global Styles and Scripts
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/GlobalAssets/Stylings/bootstrap.css",
                      "~/GlobalAssets/Stylings/dataTables.bootstrap5.css",
                      "~/GlobalAssets/Stylings/themes/base/jquery-ui.min.css",
                      "~/GlobalAssets/Stylings/site.css"));

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
                      "~/GlobalAssets/ScriptFiles/productdetail.js",
                      "~/GlobalAssets/ScriptFiles/tbs_emp.js",
                      "~/GlobalAssets/ScriptFiles/accemp.js",
                      "~/GlobalAssets/ScriptFiles/script.js"));
            
            //All Local Styles and Scripts
            bundles.Add(new Bundle("~/bundles/localscripts").Include(
                      "~/Areas/HaitiEmployee/LocalAssets/haitiemployee.js"));
        }
    }
}
