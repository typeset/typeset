using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Typeset.Web.Configuration;

namespace Typeset.Web.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected virtual string SitePath { get; private set; }
        protected virtual string ConfigPath { get; private set; }
        protected virtual string PostPath { get; private set; }
        protected virtual string IncludesPath { get; private set; }
        protected virtual IConfigurationManager ConfigurationManager { get; private set; }

        public BaseApiController(IConfigurationManager configurationManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            ConfigurationManager = configurationManager;
        }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            var appDataSitePath = ConfigurationManager.AppSettings["AppData_Site_Path"];
            var appDataSiteConfigPath = ConfigurationManager.AppSettings["AppData_Site_Config_Path"];
            var appDataSiteLayoutsPath = ConfigurationManager.AppSettings["AppData_Site_Layouts_Path"];
            var appDataSitePostsPath = ConfigurationManager.AppSettings["AppData_Site_Posts_Path"];
            var appDataSiteIncludesPath = ConfigurationManager.AppSettings["AppData_Site_Includes_Path"];

            SitePath = HttpContext.Current.Server.MapPath(appDataSitePath);
            ConfigPath = HttpContext.Current.Server.MapPath(appDataSiteConfigPath);
            PostPath = HttpContext.Current.Server.MapPath(appDataSitePostsPath);
            IncludesPath = HttpContext.Current.Server.MapPath(appDataSiteIncludesPath);

            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}