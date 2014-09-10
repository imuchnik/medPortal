using System.Web;
using System.Web.Optimization;

namespace MedicationTherapyManagement
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery-widgets").Include(
                        "~/Scripts/bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular-min.js",
                        "~/Scripts/ngDialog.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-route").Include(
                     "~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
                     "~/Scripts/underscore-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo-angular").Include(
                        "~/Scripts/kendo.web.min.js",
                        "~/Scripts/angular-kendo.js"));

            bundles.Add(new ScriptBundle("~/bundles/portal-app").Include(
                      "~/PortalApp/src/mtm-app.js",
                      "~/PortalApp/src/userService.js",
                      "~/PortalApp/src/providerService.js",
                      "~/PortalApp/src/mainController.js",
                      "~/PortalApp/src/providersController.js",
                      "~/PortalApp/src/mcaController.js",
                      "~/PortalApp/src/triggersController.js",
                      "~/PortalApp/src/casesController.js",
                      "~/PortalApp/src/caseViewController.js",
                      "~/PortalApp/src/triggerReportController.js", 
                      "~/PortalApp/src/customTriggerController.js",
                      "~/PortalApp/src/triggerWizardController.js",
                      "~/Scripts/ui-bootstrap-tpls-0.10.0.min.js",
                      "~/Scripts/bootstrap-datepicker.js"
                      ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/ngDialog.css",
                      "~/Content/ngDialog-theme-default.css",
                      "~/Content/ngDialog-theme-plain.css",
                      "~/Content/site.css", 
                      "~/Content/bootstrap-select.css",
                      "~/Content/kendo.bootstrap.min.css",
                      "~/Content/kendo.common-bootstrap.min.css", 
                      "~/Content/kendo.default.min.css",
                     "~/Content/kendo.rtl.min.css",
                      "~/Content/kendo.uniform.min.css",
                      "~/Content/kendo.silver.min.css",
                   "~/Content/bootstrap-datepicker.css"));
        }
    }
}
