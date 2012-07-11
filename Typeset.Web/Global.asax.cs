using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Typeset.Web.Configuration;

namespace Typeset.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = IocConfig.RegisterDependencies();

            AreaRegistration.RegisterAllAreas();
            FormattersConfig.RegisterFormatters();
            DependecyResolverConfig.RegisterDependencyResolver(container.Resolve<System.Web.Http.Dependencies.IDependencyResolver>());
            ControllerBuilderConfig.RegisterControllerFactory(container.Resolve<IControllerFactory>());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEnginesConfig.RegisterViewEngines();
            SiteRepositoryConfig.CloneRepository(container.Resolve<IConfigurationManager>());
        }

        public override void Init()
        {
            ExceptionHandlingConfig.RegisterEvents(this);
            ResponseTimeHeaderConfig.RegisterEvents(this);
        }
    }
}