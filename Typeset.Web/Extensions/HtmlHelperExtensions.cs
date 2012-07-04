using System.IO;
using Typeset.Web.Models.Common;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Posts;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
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

        public static string MarkupDate(this HtmlHelper helper, DateViewModel date)
        {
            return string.Format("{0}-{1}-{2}", date.Year, date.Month.ToString("00"), date.Day.ToString("00"));
        }

        public static string FormatDate(this HtmlHelper helper, ConfigurationViewModel configViewModel, DateViewModel date)
        {
            var formattedDate = string.Empty;
            DateTime dateTimeParsed;
            if (DateTime.TryParse(string.Format("{0}-{1}-{2}", date.Month, date.Day, date.Year), out dateTimeParsed))
            {
                try
                {
                    formattedDate = dateTimeParsed.ToString(configViewModel.DateFormat);
                }
                catch
                {
                    formattedDate = dateTimeParsed.ToShortDateString();
                }
            }
            return formattedDate;
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
                var absolutePath = helper.ViewContext.HttpContext.Server.MapPath(relativePath);
                if (File.Exists(absolutePath))
                {
                    html = File.ReadAllText(absolutePath);
                }
            }
            catch { }

            return new HtmlString(html);
        }
    }
}