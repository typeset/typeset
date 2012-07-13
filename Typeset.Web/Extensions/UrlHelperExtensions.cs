using System;

namespace System.Web.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string ToPublicUrl(this UrlHelper urlHelper, Uri relativeUri)
        {
            var httpContext = urlHelper.RequestContext.HttpContext;

            var uriBuilder = new UriBuilder
            {
                Host = httpContext.Request.Url.Host,
                Path = "/",
                Port = 80,
                Scheme = "http",
            };

            if (httpContext.Request.IsLocal)
            {
                uriBuilder.Port = httpContext.Request.Url.Port;
            }

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }

        public static string ToPublicUrl(this UrlHelper urlHelper, string relativeUri)
        {
            var httpContext = urlHelper.RequestContext.HttpContext;

            var uriBuilder = new UriBuilder
            {
                Host = httpContext.Request.Url.Host,
                Path = "/",
                Port = 80,
                Scheme = "http",
            };

            if (httpContext.Request.IsLocal)
            {
                uriBuilder.Port = httpContext.Request.Url.Port;
            }

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }

        public static string Content(this UrlHelper helper, string contentPath, bool absolute)
        {
            var url = string.Empty;
            
            if (!absolute)
            {
                url = helper.Content(contentPath);
            }
            else
            {
                url = helper.ToPublicUrl(contentPath);
            }

            return url;
        }
    }
}