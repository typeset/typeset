using System.Linq;
using Microsoft.Web.Helpers;
using Typeset.Web.Models.About;
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

                var aTag = new TagBuilder("a");
                aTag.Attributes.Add("href", helper.UrlHelper().RouteUrl("Home"));
                aTag.InnerHtml += sectionTag.ToString();

                wrapperTag.InnerHtml += aTag.ToString();
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

            foreach (var username in model.Usernames)
            {
                var aTag = new TagBuilder("a");
                aTag.AddCssClass(username.Key);
                aTag.Attributes.Add("href", username.Value);
                aTag.InnerHtml = username.Key;
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

        public static HtmlString GenerateComments(this HtmlHelper helper, PostViewModel postViewModel, ConfigurationViewModel configViewModel)
        {
            var html = string.Empty;
            if (!string.IsNullOrWhiteSpace(configViewModel.DisqusShortname))
            {
                var disqus_identifier = postViewModel.Permalink;
                var disqus_url = helper.UrlHelper().Content(postViewModel.Permalink, true);
                html += "<div id=\"disqus_thread\"></div><script type=\"text/javascript\">var disqus_identifier= '" + disqus_identifier + "';var disqus_url= '" + disqus_url + "';var disqus_shortname = '" + configViewModel.DisqusShortname + "';(function() {var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';(document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);})();</script><noscript>Please enable JavaScript to view the <a href=\"http://disqus.com/?ref_noscript\">comments powered by Disqus.</a></noscript><a href=\"http://disqus.com\" class=\"dsq-brlink\">comments powered by <span class=\"logo-disqus\">Disqus</span></a>";
            }
            return new HtmlString(html);
        }

        public static HtmlString GenerateCommentsCount(this HtmlHelper helper, PostViewModel postViewModel, ConfigurationViewModel viewModel)
        {
            var html = string.Empty;
            if (!string.IsNullOrWhiteSpace(viewModel.DisqusShortname))
            {
                var disqus_identifier = postViewModel.Permalink;
                var aTag = new TagBuilder("a");
                aTag.Attributes.Add("data-disqus-identifier", disqus_identifier);
                aTag.Attributes.Add("href", string.Format("{0}#disqus_thread", postViewModel.Permalink));
                html = aTag.ToString();
            }
            return new HtmlString(html);
        }
    }
}