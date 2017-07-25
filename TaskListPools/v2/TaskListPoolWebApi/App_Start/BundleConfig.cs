using System.Web;
using System.Web.Optimization;

namespace TaskListPoolWebApi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //JavaScript Scripts
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                //Angular Scripts
                "~/Scripts/modernizr-*",
                "~/Scripts/angular.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-cookies.js",
                "~/Scripts/angular-animate.js",
                //angular-ui
                "~/Scripts/angular-ui/ui-bootstrap.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                //Vue.js
                "~/Scripts/vue.min.js",
                //Other Third party scripts
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/underscore.js",
                "~/Scripts/toaster.js", //This is for angular only
                "~/Scripts/date.js",
                "~/Scripts/underscore.js",
                "~/Scripts/axios.min.js",
                "~/Scripts/toastr.min.js",
                "~/Scripts/require.js",
                //Custom scripts for this application
                "~/Scripts/Services/*.js",
                "~/MyApp/*.js", //app and app.init
                "~/MyApp/configs/*.js",
                "~/MyApp/controllers/*.js",
                "~/MyApp/services/*.js",
                "~/MyApp/Directives/*.js",
                "~/MyApp/Filters/*.js"
            ));

            //CSS Scripts
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/toaster.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
