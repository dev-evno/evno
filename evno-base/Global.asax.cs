using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC01
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Extension/Style").Include(
                "~/Content/Sheets/bootstrap.min.css"
                , "~/Content/Sheets/universal.css"));
            bundles.Add(new ScriptBundle("~/Content/Extension/Script").Include(
                "~/Content/Scripts/jquery.min.js"
                , "~/Content/Scripts/conflictRemover.js"
                , "~/Content/Scripts/angular.min.js"
                , "~/Content/Scripts/universal.js"
                ));
            //BundleTable.EnableOptimizations = true;
        }
    }

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "dash", action = "Dash", id = UrlParameter.Optional }
            );
        }
    }
}
