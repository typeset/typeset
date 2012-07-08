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
            ContentPath = HttpContext.Server.MapPath("~/App_Data/site");
            ConfigPath = HttpContext.Server.MapPath("~/App_Data/site/_config.yml");
            PostPath = HttpContext.Server.MapPath("~/App_Data/site/_posts");
            IncludesPath = HttpContext.Server.MapPath("~/App_Data/site/_includes");
        }

        protected string GetLayoutPath(string name)
        {
            return HttpContext.Server.MapPath(string.Format("~/App_Data/site/_layouts/{0}.yml", name));
        }
    }
}
