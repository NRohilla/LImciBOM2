using System.Web;
using System.Web.Optimization;

namespace OnlineBOM
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/bootstrap.bundle.js",
                        "~/Scripts/OnlineBOM/Cycle.js",
                        "~/Scripts/jquery.formatCurrency-1.4.0.js",
                         "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.validate.min.js"));

              // Use the development version of Modernizr to develop with and learn from. Then, when you're
              // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
              bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/css/sb-admin-2.min.css",
                      "~/Content/bootstrap.css",
                      //"~/Content/site.css",
                      "~/Content/toastr.css",
                      "~/Content/DataTables/css/jquery.dataTables.min.css",
                      "~/Content/font-awesome.min.css"
                      ));
        }
    }
}
