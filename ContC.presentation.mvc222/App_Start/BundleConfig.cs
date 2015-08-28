using System.Web;
using System.Web.Optimization;

namespace ContC.presentation.mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/qtip/jquery.qtip.js",
                        "~/Scripts/qtip/jquery.imagesloaded.pkg.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/globalize/globalize.js",
                        "~/Scripts/globalize/cultures/globalize.culture.pt-BR.js"
                        ));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/DatePickerReady.js",
                      "~/Scripts/DialogReady.js", "~/Scripts/jquery.waiting.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-social.css",
                      "~/Content/font-awesome.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/site.css",
                      "~/Content/jquery.waiting.css",
                      "~/Content/jquery.qtip.min.css"));
            
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                                                "~/Content/themes/base/accordion.css",
                                                "~/Content/themes/base/all.css",
                                                "~/Content/themes/base/autocomplete.css",
                                                "~/Content/themes/base/base.css",
                                                "~/Content/themes/base/button.css",
                                                "~/Content/themes/base/core.css",
                                                "~/Content/themes/base/datepicker.css",
                                                "~/Content/themes/base/dialog.css",
                                                "~/Content/themes/base/draggable.css",
                                                "~/Content/themes/base/menu.css",
                                                "~/Content/themes/base/progressbar.css",
                                                "~/Content/themes/base/resizable.css",
                                                "~/Content/themes/base/selectable.css",
                                                "~/Content/themes/base/selectmenu.css",
                                                "~/Content/themes/base/slider.css",
                                                "~/Content/themes/base/sortable.css",
                                                "~/Content/themes/base/spinner.css",
                                                "~/Content/themes/base/tabs.css",
                                                "~/Content/themes/base/theme.css",
                                                "~/Content/themes/base/tooltip.css"));
        }
    }
}
