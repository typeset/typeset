using Microsoft.Web.Helpers;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Common;
using Typeset.Web.Models.Posts;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
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

            if (model.HasTwitterUsername())
            {
                var sectionTag = new TagBuilder("section");
                sectionTag.AddCssClass("twitter");

                var aTag = new TagBuilder("a");
                aTag.Attributes.Add("href", model.TwitterUrl());
                aTag.InnerHtml = "twitter";
                
                sectionTag.InnerHtml += aTag.ToString();
                wrapperTag.InnerHtml += sectionTag.ToString();
            }

            if (model.HasGithubUsername())
            {
                var sectionTag = new TagBuilder("section");
                sectionTag.AddCssClass("github");

                var aTag = new TagBuilder("a");
                aTag.Attributes.Add("href", model.GithubUrl());
                aTag.InnerHtml = "github";

                sectionTag.InnerHtml += aTag.ToString();
                wrapperTag.InnerHtml += sectionTag.ToString();
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
                var liTag = new TagBuilder("li");
                liTag.InnerHtml = string.Format("next {0}", numNext);
                olTag.InnerHtml += liTag.ToString();
            }

            if (hasPrevious)
            {
                var liTag = new TagBuilder("li");
                liTag.InnerHtml = string.Format("previous {0}", numPrevious);
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