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
        private static readonly TimeSpan TimeBetweenUpdates = new TimeSpan(0, 30, 0);
        private static DateTimeOffset LastRepositoryUpdate { get; set; }
        private static IConfigurationManager ConfigurationManager { get; set; }

        public static void RegisterEvents(HttpApplication context, IConfigurationManager configurationManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

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
                        var siteRepository = ConfigurationManager.AppSettings["SiteRepositoryUri"];

                        if (string.IsNullOrWhiteSpace(siteRepository))
                        {
                            return;
                        }

                        if (!Directory.Exists(sitePath))
                        {
                            Directory.CreateDirectory(sitePath);
                        }

                        if (siteRepository.EndsWith(".git", System.StringComparison.OrdinalIgnoreCase))
                        {
                            if (Directory.EnumerateFileSystemEntries(sitePath).Any())
                            {
                                if ((DateTimeOffset.UtcNow - LastRepositoryUpdate) > TimeBetweenUpdates)
                                {
                                    Git.Pull(sitePath);
                                    LastRepositoryUpdate = DateTimeOffset.UtcNow;
                                }
                            }
                            else
                            {
                                Git.Clone(siteRepository, sitePath);
                                LastRepositoryUpdate = DateTimeOffset.UtcNow;
                            }
                        }
                    }
                    catch { }
                });
        }
    }
}