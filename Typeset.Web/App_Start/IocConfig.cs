using System.Configuration;
using System.Web.Mvc;
using TinyIoC;
using Typeset.Domain.Post;
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
            var postRepositoryPath = ConfigurationManager.AppSettings["IPostRepository-Path"];

            //MVC Framework
            container.Register<System.Web.Http.Dependencies.IDependencyResolver, TinyIocDependencyResolver>();
            container.Register<IControllerFactory, TinyIocControllerFactory>();

            //Domain
            container.Register<string>(postRepositoryPath, "IPostRepository-Path");
            container.Register<IPostRepository>((c, n) => new PostRepository(c.Resolve<string>("IPostRepository-Path")));
            
            //WebApi Controllers
            container.Register<ValuesController>();

            //Mvc Controllers
            container.Register<IController, HomeController>("Home");

            return container;
        }
    }
}