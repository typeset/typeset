using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Typeset.Web.ViewResults
{
    public class AtomViewResult : ViewResult
    {
        private new object Model { get; set; }

        public AtomViewResult(object model)
        {
            Model = model;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            this.ViewData.Model = Model;
            base.ExecuteResult(context);
            HttpContext.Current.Response.ContentType = "application/atom+xml";
        }
    }
}