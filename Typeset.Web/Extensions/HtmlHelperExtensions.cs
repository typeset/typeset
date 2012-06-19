using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using Typeset.Web.Models.Common;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString EmailLink(this HtmlHelper helper, string emailAddress, string text)
        {
            TagBuilder tag = new TagBuilder("a");
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
    }
}