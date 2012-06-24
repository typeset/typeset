using System.Configuration;
using System.Web.Mvc;
using TinyIoC;
using Typeset.Domain.Post;
using Typeset.Web.Controllers.Api;
using Typeset.Web.Controllers.DependencyResolvers;
using Typeset.Web.Controllers.Factories;
using Typeset.Web.Controllers.Site;
using Typeset.Domain.About;
using Typeset.Domain.Markup;

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
            container.Register<IAboutRepository, AboutRepository>();
            container.Register<IPostRepository, PostRepository>();
            container.Register<IMarkupProcessorFactory, ProcessorFactory>();
            
            //WebApi Controllers
            container.Register<PostsController>();

            //Mvc Controllers
            container.Register<IController, StaticFileController>("StaticFile").AsMultiInstance();
            container.Register<IController, HomeController>("Home").AsMultiInstance();
            container.Register<IController, PostController>("Post").AsMultiInstance();

            return container;
        }
    }
}