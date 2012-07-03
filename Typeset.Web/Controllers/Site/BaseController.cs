using System.Web;
using System.Web.Mvc;

namespace Typeset.Web.Controllers.Site
{
    public class BaseController : Controller
    {
        protected virtual string ContentPath { get; private set; }
        protected virtual string ConfigPath { get; private set; }
        protected virtual string PostPath { get; private set; }
        protected virtual string IncludesPath { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            ContentPath = HttpContext.Server.MapPath("~/App_Data");
            ConfigPath = HttpContext.Server.MapPath("~/App_Data/_config.yml");
            PostPath = HttpContext.Server.MapPath("~/App_Data/_posts");
            IncludesPath = HttpContext.Server.MapPath("~/App_Data/_includes");
        }
    }
}
