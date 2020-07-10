using System.Web;
using System.Web.Optimization;

namespace CigarStoreProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/slick.min.js",
                      "~/Scripts/nouislider.min.js",
                      "~/Scripts/jquery.zoom.min.js",
                      "~/Scripts/main.js",
                      "~/Scripts/order-template.js",
                      "~/Scripts/printThis.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/slick.css",
                      "~/Content/slick-theme.css",
                      "~/Content/nouislider.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/style.css",
                      "~/Content/order-template.css"));
        }
    }
}
