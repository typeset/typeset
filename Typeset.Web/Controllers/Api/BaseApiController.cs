using System;
using System.IO;
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
        protected virtual string LayoutsPath { get; private set; }
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
            SitePath = ConfigurationManager.AppSettings["SitePath"];
            if (!Path.IsPathRooted(SitePath))
            {
                SitePath = HttpContext.Current.Server.MapPath(SitePath);
            }
            ConfigPath = Path.Combine(SitePath, "_config.yml");
            LayoutsPath = Path.Combine(SitePath, "_layouts");
            PostPath = Path.Combine(SitePath, "_posts");
            IncludesPath = Path.Combine(SitePath, "_includes");

            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}