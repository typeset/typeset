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
            routes.IgnoreRoute("favicon.ico");

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
                name: "Permalink",
                url: "{*permalink}",
                defaults: new { controller = "Post", action = "Get", permalink = UrlParameter.Optional }
            );
        }
    }
}