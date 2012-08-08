using System;
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
            var appDataSitePath = ConfigurationManager.AppSettings["AppData_Site_Path"];
            var appDataSiteConfigPath = ConfigurationManager.AppSettings["AppData_Site_Config_Path"];
            var appDataSiteLayoutsPath = ConfigurationManager.AppSettings["AppData_Site_Layouts_Path"];
            var appDataSitePostsPath = ConfigurationManager.AppSettings["AppData_Site_Posts_Path"];
            var appDataSiteIncludesPath = ConfigurationManager.AppSettings["AppData_Site_Includes_Path"];

            SitePath = HttpContext.Server.MapPath(appDataSitePath);
            ConfigPath = HttpContext.Server.MapPath(appDataSiteConfigPath);
            PostPath = HttpContext.Server.MapPath(appDataSitePostsPath);
            IncludesPath = HttpContext.Server.MapPath(appDataSiteIncludesPath);
        }

        protected string GetLayoutPath(string name)
        {
            var appDataSiteLayoutsPath = ConfigurationManager.AppSettings["AppData_Site_Layouts_Path"];
            return HttpContext.Server.MapPath(string.Format("{0}/{1}.yml", appDataSiteLayoutsPath, name));
        }
    }
}
