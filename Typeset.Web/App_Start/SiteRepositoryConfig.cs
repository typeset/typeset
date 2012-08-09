using System;
using System.Threading.Tasks;
using System.Web;
using Typeset.Domain.Repository;
using Typeset.Web.Configuration;

namespace Typeset.Web
{
    public static class SiteRepositoryConfig
    {
        public static void CheckoutOrUpdate(HttpApplication context, IConfigurationManager configurationManager, IRepositoryManager repositoryManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            if (repositoryManager == null)
            {
                throw new ArgumentNullException("repositoryManager");
            }

            var repositoryUri = configurationManager.AppSettings["SiteRepositoryUri"];
            var path = context.Server.MapPath(configurationManager.AppSettings["AppData_Site_Path"]);

            try
            {
                repositoryManager.CheckoutOrUpdate(repositoryUri, path);
            }
            catch { }
        }
    }
}