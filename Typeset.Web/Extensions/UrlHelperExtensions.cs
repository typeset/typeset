using System;

namespace System.Web.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string Content(this UrlHelper helper, string contentPath, bool absolute)
        {
            if (!absolute)
            {
                return helper.Content(contentPath);
            }
            else
            {
                var request = helper.RequestContext.HttpContext.Request;
                return string.Format("{0}://{1}/{2}", request.Url.Scheme, request.Url.Authority, helper.Content(contentPath));
            }
        }
    }
}