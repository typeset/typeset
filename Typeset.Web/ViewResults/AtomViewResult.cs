using System.Web;
using System.Web.Mvc;

namespace Typeset.Web.ViewResults
{
    public class AtomViewResult : ViewResult
    {
        private new object Model { get; set; }

        public AtomViewResult(object model)
        {
            Model = model;
        }

        public AtomViewResult(string viewName, object model)
        {
            Model = model;
            this.ViewName = viewName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            this.ViewData.Model = Model;
            base.ExecuteResult(context);
            HttpContext.Current.Response.ContentType = "application/atom+xml";
        }
    }
}