﻿using System.Web;
using System.Web.Optimization;

namespace TaskListPoolWebApi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //JavaScript Scripts
            bundles.Add(new ScriptBundle("~/bundles/jsfa").Include(
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
                //Other Third party scripts
                "~/Scripts/toaster.js", //This is for angular only
                //Custom scripts for this application
                "~/MyApp/*.js", //app and app.init
                "~/MyApp/configs/*.js",
                "~/MyApp/controllers/*.js",
                "~/MyApp/services/*.js",
                "~/MyApp/Directives/*.js",
                "~/MyApp/Filters/*.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jsfv").Include(
                //Vue.js
                "~/Scripts/vue.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                //Other Third party scripts
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js", //The order matters, this has to come after jQuery
                //"~/Scripts/jquery.multi-select.js",
                "~/Scripts/jquery.sumoselect.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/date.js", 
                "~/Scripts/underscore.js", //Lib for collections
                "~/Scripts/axios.min.js", //HTTP lib for any framework
                "~/Scripts/toastr.min.js", //Regular JS toaster
                "~/Scripts/require.js",
                //Custom scripts for this application
                "~/Scripts/Services/*.js"
            ));
            
            //CSS Scripts
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-glyphicons.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/toaster.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/themes/base/theme.css",
                      "~/Content/themes/base/dialog.css",
                      //"~/Content/multi-select.css",
                      "~/Content/sumoselect.css",
                      "~/Content/sumoselect-overrides.css"
            ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
