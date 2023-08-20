﻿using System.Web;
using System.Web.Optimization;

namespace YTP.Main {
    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/dataTables.bootstrap5.css",
                      "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                        "~/Scripts/jquery-ui-1.13.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap5.min.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new Bundle("~/bundles/scripts").Include(
                      "~/Scripts/productdetail.js",
                      "~/Scripts/tbs_emp.js",
                      "~/Scripts/script.js"));
        }
    }
}
