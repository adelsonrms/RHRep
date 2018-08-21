using System.Web.Optimization;

namespace ERP.Presentation.Api
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/tecnun").Include(
                "~/content/tecnun.css"));

            //Componentes JS e CSS para o Bootstrap
            bundles.Add(new StyleBundle("~/css/Bootstrap").Include(
                "~/Content/bootstrap/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/js/Bootstrap").Include(
                "~/Content/bootstrap/bootstrap.js"));

            //Componentes JS e CSS para o JQuery
            bundles.Add(new ScriptBundle("~/JS/JQuery").Include(
                "~/Content/jquery/jquery-1.12.4.js",
                "~/Content/jquery/jquery.maskedinput.js"));

            //Componentes JS e CSS para o DataTable
            bundles.Add(new ScriptBundle("~/JS/DataTable").Include(
                "~/Content/datatable/js/dataTables.bootstrap.min.js",
                "~/Content/datatable/js/jquery.dataTables.js"));

            bundles.Add(new StyleBundle("~/CSS/DataTable").Include(
                "~/Content/datatable/css/jquery.dataTables.css"));

            bundles.Add(new StyleBundle("~/SELECT2").Include(
                "~/Content/select2/select2.min.css",
                "~/Content/select2/select2-bootstrap.min.css"));

            

            bundles.Add(new ScriptBundle("~/SELECT2").Include("~/Content/select2/select2.min.js"));

        }
    }
}