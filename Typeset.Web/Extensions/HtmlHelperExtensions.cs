using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Optimization;
using Typeset.Web.CoffeeScript.BundleTransforms;
using Typeset.Web.Less.BundleTransforms;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Posts;
using Typeset.Web.Sass.BundleTransforms;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        private static Version AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
        private static string AssemblyVersionString = string.Format("{0}.{1}.{2}", AssemblyVersion.Major, AssemblyVersion.Minor, AssemblyVersion.Build);

        public static string GetAssemblyVersion(this HtmlHelper helper)
        {
            return AssemblyVersionString;
        }

        public static UrlHelper UrlHelper(this HtmlHelper helper)
        {
            return new UrlHelper(helper.ViewContext.RequestContext);
        }

        public static HtmlString EmailLink(this HtmlHelper helper, string emailAddress, string text)
        {
            var tag = new TagBuilder("a");
            tag.Attributes.Add("href", string.Format("mailto:{0}", emailAddress));
            tag.InnerHtml = text;
            return new HtmlString(tag.ToString());
        }

        public static string Html5DateTime(this HtmlHelper helper, DateTimeOffset dateTime)
        {
            return string.Format("{0:yyyy-MM-dd}T{0:hh:mm:ss}Z", dateTime);
        }

        public static string FormatDate(this HtmlHelper helper, ConfigurationViewModel configViewModel, DateTimeOffset dateTime)
        {
            try
            {
                return dateTime.ToString(configViewModel.DateFormat);
            }
            catch
            {
                return dateTime.ToString("g"); // format: 3/9/2008 4:05 PM
            }
        }

        public static HtmlString GeneratePagination(this HtmlHelper helper, PageOfPostsViewModel page)
        {
            var hasPrevious = page.SearchCriteria.Offset > 0;
            var numPreviousTotal = page.SearchCriteria.Offset;
            var numPrevious = numPreviousTotal > page.SearchCriteria.Limit ? page.SearchCriteria.Limit : numPreviousTotal;
            var hasNext = (page.SearchCriteria.Offset + page.SearchCriteria.Limit) < page.TotalCount;
            var numNextTotal = page.TotalCount - (page.SearchCriteria.Offset + page.SearchCriteria.Limit);
            var numNext = numNextTotal > page.SearchCriteria.Limit ? page.SearchCriteria.Limit : numNextTotal;

            var olTag = new TagBuilder("ol");

            if (hasNext)
            {
                var limit = page.SearchCriteria.Limit;
                var offset = page.SearchCriteria.Offset + page.SearchCriteria.Limit;

                var aTag = new TagBuilder("a");
                aTag.InnerHtml = string.Format("next {0}", numNext);
                aTag.Attributes.Add("href", helper.UrlHelper().RouteUrl("Home", new { limit = limit, offset = offset }));

                var liTag = new TagBuilder("li");
                liTag.AddCssClass("next");
                liTag.InnerHtml += aTag.ToString();
                olTag.InnerHtml += liTag.ToString();
            }

            if (hasPrevious)
            {
                var limit = page.SearchCriteria.Limit;
                var offset = page.SearchCriteria.Offset - page.SearchCriteria.Limit;
                offset = offset < 0 ? 0 : offset;

                var aTag = new TagBuilder("a");
                aTag.InnerHtml = string.Format("previous {0}", numPrevious);
                aTag.Attributes.Add("href", helper.UrlHelper().RouteUrl("Home", new { limit = limit, offset = offset }));

                var liTag = new TagBuilder("li");
                liTag.AddCssClass("previous");
                liTag.InnerHtml += aTag.ToString();
                olTag.InnerHtml += liTag.ToString();
            }

            var navTag = new TagBuilder("nav");
            navTag.InnerHtml += olTag.ToString();

            var sectionTag = new TagBuilder("section");
            sectionTag.AddCssClass("pagination");
            sectionTag.InnerHtml += navTag.ToString();

            return new HtmlString(sectionTag.ToString());
        }

        public static HtmlString Include(this HtmlHelper helper, string relativePath)
        {
            var html = string.Empty;

            try
            {
                var absolutePath = helper.ViewContext.HttpContext.Server.MapPath(string.Format("~/App_Data/site{0}", relativePath));
                if (File.Exists(absolutePath))
                {
                    html = File.ReadAllText(absolutePath);
                }
            }
            catch { }

            return new HtmlString(html);
        }

        public static HtmlString CompileBundleAndMinifyScripts(this HtmlHelper helper, string name, IEnumerable<string> paths)
        {
            var html = new HtmlString(string.Empty);

            if (paths != null && paths.Any())
            {
                BundleTable.EnableOptimizations = true;
                BundleTable.Bundles.IgnoreList.Clear();
                BundleTable.Bundles.FileSetOrderList.Clear();
                BundleTable.Bundles.FileExtensionReplacementList.Clear();

                var javaScript = paths.Select(p => string.Format("~/App_Data/site{0}", p)).ToArray();
                var virtualPath = string.Format("~/scripts/javascript/{0}", helper.UrlHelper().Encode(name));
                
                var bundle = new Bundle(virtualPath);
                bundle.Include(javaScript);
                bundle.Transforms.Add(new CoffeeScriptCompile());
                bundle.Transforms.Add(new JsMinify());
                BundleTable.Bundles.Add(bundle);

                var scriptTag = new TagBuilder("script");
                scriptTag.Attributes.Add("type", "text/javascript");
                scriptTag.Attributes.Add("src", BundleTable.Bundles.ResolveBundleUrl(virtualPath));
                html = new HtmlString(scriptTag.ToString());
            }

            return html;
        }

        public static HtmlString CompileBundleAndMinifyStyles(this HtmlHelper helper, string name, IEnumerable<string> paths)
        {
            var html = new HtmlString(string.Empty);

            if (paths != null && paths.Any())
            {
                BundleTable.EnableOptimizations = true;
                BundleTable.Bundles.IgnoreList.Clear();
                BundleTable.Bundles.FileSetOrderList.Clear();
                BundleTable.Bundles.FileExtensionReplacementList.Clear();

                var virtualPath = string.Format("~/styles/css/{0}", helper.UrlHelper().Encode(name));
                var bundle = new Bundle(virtualPath);
                bundle.Transforms.Add(new SassCompile());
                bundle.Transforms.Add(new LessCompile());
                bundle.Transforms.Add(new CssMinify());
                bundle.Include(paths.Select(p => string.Format("~/App_Data/site{0}", p)).ToArray());
                BundleTable.Bundles.Add(bundle);

                var scriptTag = new TagBuilder("link");
                scriptTag.Attributes.Add("type", "text/css");
                scriptTag.Attributes.Add("rel", "stylesheet");
                scriptTag.Attributes.Add("href", BundleTable.Bundles.ResolveBundleUrl(virtualPath));
                html = new HtmlString(scriptTag.ToString(TagRenderMode.SelfClosing));
            }

            return html;
        }
    }
}