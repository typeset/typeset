using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Typeset.Web.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected virtual string ContentPath { get; private set; }
        protected virtual string ConfigPath { get; private set; }
        protected virtual string PostPath { get; private set; }
        protected virtual string IncludesPath { get; private set; }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            ContentPath = HttpContext.Current.Server.MapPath("~/App_Data/site");
            ConfigPath = HttpContext.Current.Server.MapPath("~/App_Data/site/_config.yml");
            PostPath = HttpContext.Current.Server.MapPath("~/App_Data/site/_posts");
            IncludesPath = HttpContext.Current.Server.MapPath("~/App_Data/site/_includes");

            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}