using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Typeset.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = IocConfig.RegisterDependencies();

            AreaRegistration.RegisterAllAreas();
            DependecyResolverConfig.RegisterDependencyResolver(container.Resolve<System.Web.Http.Dependencies.IDependencyResolver>());
            ControllerBuilderConfig.RegisterControllerFactory(container.Resolve<IControllerFactory>());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void Init()
        {
            ExceptionHandlingConfig.RegisterEvents(this);
        }
    }
}