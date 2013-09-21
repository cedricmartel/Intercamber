using System.Web.Optimization;

namespace CML.Intercamber.Web.App_Start
{
    public class BundleConfig
    {
        // Pour plus d'informations sur Bundling, accédez à l'adresse http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libjquery").Include(
                "~/Scripts/jquery-1.8.3.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/libjqueryui").Include(
                        "~/Scripts/jquery-ui-1.9.2.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/intercamber").Include("~/Scripts/Intercamber.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/jquery-ui-1.9.2.custom.min.css",
                        "~/Content/site.css"
                        ));
        }
    }
}