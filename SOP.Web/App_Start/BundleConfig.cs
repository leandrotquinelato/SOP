using System.Web;
using System.Web.Optimization;

namespace SOP.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/ComponentesPersonalizados").Include(
                        "~/Content/ComponentesPersonalizados/autoCompletePatioFerroviario.css",
                        "~/Content/ComponentesPersonalizados/autoCompleteTerminalFerroviario.css",
                        "~/Content/ComponentesPersonalizados/autoCompleteTrem.css",
                        "~/Content/ComponentesPersonalizados/autoCompleteVeiculoFerroviario.css"));

            //Importação do estilo necessário para renderizar a timeline
            bundles.Add(new StyleBundle("~/Content/timelineCss").Include(
                            "~/Content/timeline.css"));

            bundles.Add(new StyleBundle("~/Content/core").Include(
                "~/Content/core/css/font-awesome.min.css",
                "~/Content/core/css/bootstrap.min.css",
                "~/Content/core/css/style-conquer.css",
                "~/Content/core/css/style.css",
                "~/Content/core/css/style-responsive.css",
                "~/Content/core/css/light.css",
                "~/Content/core/css/mission-gothic.css",
                "~/Content/core/css/core.css"));

            bundles.Add(new ScriptBundle("~/Scripts/netlog").Include(
                //   "~/Content/core/js/jquery-1.10.2.min.js",
                //   "~/Content/core/js/jquery-migrate-1.2.1.min.js",
                "~/Content/core/js/bootstrap.min.js",
                "~/Content/core/js/jquery.slimscroll.min.js",
                "~/Content/core/js/jquery.cokie.min.js",
                "~/Content/core/js/app.js"));

            bundles.Add(new ScriptBundle("~/Scripts/kendo").Include(
                "~/Scripts/kendo/2014.2.716/jquery.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.all.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.aspnetmvc.min.js",
                "~/Scripts/kendo.modernizr.custom.js",
                "~/Scripts/kendo/2014.2.716/cultures/kendo.culture.pt-BR.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                "~/Content/kendo/2014.2.716/kendo.common.min.css",
                "~/Content/kendo/2014.2.716/kendo.dataviz.min.css",
                "~/Content/kendo/2014.2.716/kendo.bootstrap.min.css",
                "~/Content/kendo/2014.2.716/kendo.dataviz.bootstrap.min.css"));
        }
    }
}
