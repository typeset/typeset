using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Typeset.Domain.Repository;
using Typeset.Web.Configuration;

namespace Typeset.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = IocConfig.RegisterDependencies();

            MvcHandler.DisableMvcResponseHeader = true;
            FormattersConfig.RegisterFormatters();
            DependecyResolverConfig.RegisterDependencyResolver(container.Resolve<System.Web.Http.Dependencies.IDependencyResolver>());
            ControllerBuilderConfig.RegisterControllerFactory(container.Resolve<IControllerFactory>());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEnginesConfig.RegisterViewEngines();
        }

        public override void Init()
        {
            var container = IocConfig.RegisterDependencies();

            ExceptionHandlingConfig.RegisterEvents(this);
            ResponseTimeHeaderConfig.RegisterEvents(this);
            SiteRepositoryConfig.CheckoutOrUpdate(this, container.Resolve<IConfigurationManager>(), container.Resolve<IRepositoryManager>());
        }
    }
}