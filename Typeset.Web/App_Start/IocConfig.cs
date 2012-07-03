using System.Web.Mvc;
using TinyIoC;
using Typeset.Domain.Configuration;
using Typeset.Domain.FrontMatter;
using Typeset.Domain.Markup;
using Typeset.Web.Controllers.Api;
using Typeset.Web.Controllers.DependencyResolvers;
using Typeset.Web.Controllers.Factories;
using Typeset.Web.Controllers.Site;

namespace Typeset.Web
{
    public class IocConfig
    {
        public static TinyIoCContainer RegisterDependencies()
        {
            var container = new TinyIoCContainer();

            //App Settings

            //MVC Framework
            container.Register<System.Web.Http.Dependencies.IDependencyResolver, TinyIocDependencyResolver>();
            container.Register<IControllerFactory, TinyIocControllerFactory>();

            //Domain
            container.Register<IConfigurationRepository, ConfigurationRepository>();
            container.Register<IFrontMatterRepository, FrontMatterRepository>();
            container.Register<IMarkupProcessorFactory, MarkupProcessorFactory>();
            
            //WebApi Controllers
            container.Register<PostsController>();

            //Mvc Controllers
            container.Register<IController, UrlController>("Url").AsMultiInstance();
            container.Register<IController, HomeController>("Home").AsMultiInstance();

            return container;
        }
    }
}