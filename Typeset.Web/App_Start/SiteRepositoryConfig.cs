using System;
using System.IO;
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
            var path = configurationManager.AppSettings["Site_Path"];
            if (!Path.IsPathRooted(path))
            {
                path = context.Server.MapPath(path);
            }

            try
            {
                repositoryManager.CheckoutOrUpdate(repositoryUri, path);
            }
            catch { }
        }
    }
}