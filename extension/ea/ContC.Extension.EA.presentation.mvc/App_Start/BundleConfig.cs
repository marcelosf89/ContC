using System.Web;
using System.Web.Optimization;

namespace ContC.Extension.EA.presentation.mvc
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
                        "~/Scripts/qtip/jquery.imagesloaded.pkg.min.js",
                        "~/Scripts/jquery.waiting.js"));

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
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css-smart").Include(
                                  "~/Content/smartadmin-production.min.css",
                      "~/Content/smartadmin-skins.min.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",


                      "~/Content/contc.min.css",

                      "~/Content/site.css"));


            bundles.Add(new ScriptBundle("~/bundles/contc").Include(
          "~/Scripts/app.config.js",

          "~/Scripts/notification/SmartNotification.min.js",
          "~/Scripts/smartwidgets/jarvis.widget.min.js",
          "~/Scripts/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
          "~/Scripts//plugin/sparkline/jquery.sparkline.min.js",
          "~/Scripts/plugin/masked-input/jquery.maskedinput.min.js",
          "~/Scripts/plugin/select2/select2.min.js",
          "~/Scripts/plugin/bootstrap-slider/bootstrap-slider.min.js",
          "~/Scripts/plugin/msie-fix/jquery.mb.browser.min.js",
          "~/Scripts/plugin/fastclick/fastclick.min.js",
          "~/Scripts/contc.min.js",
          "~/Scripts/app.min.js",
          "~/Scripts/speech/voicecommand.min.js",
          "~/Scripts/plugin/flot/jquery.flot.cust.min.js",
          "~/Scripts/plugin/flot/jquery.flot.resize.min.js",
          "~/Scripts/plugin/flot/jquery.flot.tooltip.min.js",
          "~/Scripts/plugin/vectormap/jquery-jvectormap-1.2.2.min.js",
          "~/Scripts/plugin/vectormap/jquery-jvectormap-world-mill-en.js",
          "~/Scripts/plugin/fullcalendar/jquery.fullcalendar.min.js",

          "~/Scripts/plugin/jquery-touch/jquery.ui.touch-punch.min.js"));
        }
    }
}
