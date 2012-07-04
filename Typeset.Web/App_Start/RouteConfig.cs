using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Typeset.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Get", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Atom",
                url: "atom.xml",
                defaults: new { controller = "Url", action = "Atom" }
            );

            routes.MapRoute(
                name: "Unknown",
                url: "{*url}",
                defaults: new { controller = "Url", action = "Get", url = UrlParameter.Optional }
            );
        }
    }
}