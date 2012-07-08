using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Typeset.Domain.Git;

namespace Typeset.Web
{
    public class SiteRepositoryConfig
    {
        private const string VirtualSitePath = "~/App_Data/site";
        private static string siteRepository = ConfigurationManager.AppSettings["SiteRepository"];

        public static void CloneRepository()
        {
            string sitePath = HttpContext.Current.Server.MapPath(VirtualSitePath);
            
            try
            {
                if (!Directory.Exists(sitePath))
                {
                    Directory.CreateDirectory(sitePath);

                    if (siteRepository.EndsWith(".git", System.StringComparison.OrdinalIgnoreCase))
                    {
                        Git.Clone(siteRepository, sitePath);
                    }
                }
            }
            catch { }
        }

        public static void RegisterEvents(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new System.EventHandler(context_PreRequestHandlerExecute);
        }

        private static void context_PreRequestHandlerExecute(object sender, System.EventArgs e)
        {
            string sitePath = HttpContext.Current.Server.MapPath(VirtualSitePath);
            Task.Factory.StartNew(() => Git.Pull(sitePath));
        }
    }
}