﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Typeset.Web.Configuration;

namespace Typeset.Web.Controllers.Site
{
    public class BaseController : Controller
    {
        protected const int DefaultCacheTime = 60 * 20;
        protected const OutputCacheLocation DefaultOutputCacheLocation = OutputCacheLocation.Server;
        protected virtual string SitePath { get; private set; }
        protected virtual string ConfigPath { get; private set; }
        protected virtual string LayoutsPath { get; private set; }
        protected virtual string PostPath { get; private set; }
        protected virtual string IncludesPath { get; private set; }
        protected virtual IConfigurationManager ConfigurationManager { get; private set; }

        public BaseController(IConfigurationManager configurationManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configuraitonManager");
            }

            ConfigurationManager = configurationManager;
        }

        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            SitePath = ConfigurationManager.AppSettings["Site_Path"];
            if (!Path.IsPathRooted(SitePath))
            {
                SitePath = HttpContext.Server.MapPath(SitePath);
            }
            ConfigPath = Path.Combine(SitePath, "_config.yml");
            LayoutsPath = Path.Combine(SitePath, "_layouts");
            PostPath = Path.Combine(SitePath, "_posts");
            IncludesPath = Path.Combine(SitePath, "_includes");
        }

        protected string GetLayoutPath(string name)
        {
            return Path.Combine(LayoutsPath, string.Concat(name, ".yml"));
        }
    }
}
