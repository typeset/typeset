using System.IO;
using System.Web;
using Typeset.Domain.Git;
using Typeset.Web.Configuration;

namespace Typeset.Web
{
    public class SiteRepositoryConfig
    {
        public static void CloneRepository(IConfigurationManager configurationManager)
        {
            string sitePath = HttpContext.Current.Server.MapPath(configurationManager.AppSettings["AppData_Site_Path"]);
            string siteRepository = configurationManager.AppSettings["SiteRepository"];

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
    }
}