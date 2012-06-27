using Microsoft.Web.Helpers;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Common;
using Typeset.Web.Models.Posts;
using System.Linq;

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

        public static string FormatDate(this HtmlHelper helper, DateViewModel date)
        {
            var formattedDate = string.Format("{0}-{1}-{2}", date.Month, date.Day, date.Year);
            DateTime dateTimeParsed;
            if(DateTime.TryParse(formattedDate, out dateTimeParsed))
            {
                formattedDate = dateTimeParsed.ToShortDateString();
            }
            return formattedDate;
        }

        public static HtmlString GenerateAbout(this HtmlHelper helper, AboutViewModel model)
        {
            var wrapperTag = new TagBuilder("section");
            wrapperTag.AddCssClass("about");

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                var sectionTag = new TagBuilder("section");
                sectionTag.AddCssClass("avatar");
                sectionTag.InnerHtml = Gravatar.GetHtml(model.Email, 75, null, GravatarRating.X, "jpg").ToString();
                wrapperTag.InnerHtml += sectionTag.ToString();
            }

            if (!string.IsNullOrWhiteSpace(model.FullName()))
            {
                var sectionTag = new TagBuilder("section");
                sectionTag.AddCssClass("fullname");
                sectionTag.InnerHtml = model.FullName();
                wrapperTag.InnerHtml += sectionTag.ToString();
            }

            if (!string.IsNullOrWhiteSpace(model.Bio))
            {
                var sectionTag = new TagBuilder("section");
                sectionTag.AddCssClass("bio");
                sectionTag.InnerHtml = model.Bio;
                wrapperTag.InnerHtml += sectionTag.ToString();
            }

            var sectionLinkTag = new TagBuilder("section");
            sectionLinkTag.AddCssClass("links");

            if (model.HasTwitterUsername())
            {
                var aTag = new TagBuilder("a");
                aTag.AddCssClass("twitter");
                aTag.Attributes.Add("href", model.TwitterUrl());
                aTag.InnerHtml = "twitter";
                sectionLinkTag.InnerHtml += aTag.ToString();
            }

            if (model.HasGithubUsername())
            {
                var aTag = new TagBuilder("a");
                aTag.AddCssClass("github");
                aTag.Attributes.Add("href", model.GithubUrl());
                aTag.InnerHtml = "github";
                sectionLinkTag.InnerHtml += aTag.ToString();
            }

            var aRssTag = new TagBuilder("a");
            aRssTag.AddCssClass("rss");
            aRssTag.Attributes.Add("href", helper.UrlHelper().RouteUrl(routeName: "Atom"));
            aRssTag.InnerHtml = "rss";
            sectionLinkTag.InnerHtml += aRssTag.ToString();

            if (model.Links.Any())
            {
                wrapperTag.InnerHtml += sectionLinkTag.ToString();
            }

            return new HtmlString(wrapperTag.ToString());
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
    }
}