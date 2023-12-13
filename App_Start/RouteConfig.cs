using System.Web.Mvc;
using System.Web.Routing;

namespace MvcOnlineTicariOtomasyon
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CargoDetails",
                url: "{controller}/{action}/{trackingCode}",
                defaults: new { controller = "Cargo", action = "CargoDetail", trackingCode = UrlParameter.Optional }
            );
        }
    }
}
