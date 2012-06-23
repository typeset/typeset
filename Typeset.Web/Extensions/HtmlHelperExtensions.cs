using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
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

        public static HtmlString GeneratePagination(this HtmlHelper helper, PageOfPostViewModel page)
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