using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Typeset.Domain.Git;
using Typeset.Web.Configuration;

namespace Typeset.Web
{
    public static class SiteRepositoryConfig
    {
        private static IConfigurationManager ConfigurationManager { get; set; }

        public static void RegisterEvents(HttpApplication context, IConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        private static void context_BeginRequest(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var context = (sender as HttpApplication).Context;
                        var sitePath = context.Server.MapPath(ConfigurationManager.AppSettings["AppData_Site_Path"]);
                        var siteRepository = ConfigurationManager.AppSettings["SiteRepository"];

                        if (!string.IsNullOrWhiteSpace(siteRepository))
                        {
                            if (!Directory.Exists(sitePath))
                            {
                                Directory.CreateDirectory(sitePath);
                            }

                            if (!Directory.EnumerateFileSystemEntries(sitePath).Any())
                            {
                                if (siteRepository.EndsWith(".git", System.StringComparison.OrdinalIgnoreCase))
                                {
                                    Git.Clone(siteRepository, sitePath);
                                }
                            }
                        }
                    }
                    catch { }
                });
        }
    }
}