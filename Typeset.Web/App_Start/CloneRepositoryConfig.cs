using System.Configuration;
using System.IO;
using System.Web;
using Typeset.Domain.Git;

namespace Typeset.Web
{
    public class CloneRepositoryConfig
    {
        public static void CloneRepository()
        {
            var sitePath = HttpContext.Current.Server.MapPath("~/App_Data/site");
            var siteRepository = ConfigurationManager.AppSettings["SiteRepository"];

            if (!Directory.Exists(sitePath))
            {
                Directory.CreateDirectory(sitePath);

                if (siteRepository.EndsWith(".git", System.StringComparison.OrdinalIgnoreCase))
                {
                    Git.Clone(siteRepository, sitePath);
                }
            }
        }
    }
}