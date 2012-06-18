using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Typeset.Web
{
    public class DependecyResolverConfig
    {
        public static void RegisterDependencyResolver(IDependencyResolver dependencyResolver)
        {
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
        }
    }
}