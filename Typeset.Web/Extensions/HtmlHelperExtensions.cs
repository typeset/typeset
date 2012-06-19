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
        public static string Md5Hash(this HtmlHelper helper, string input)
        {
            var hashedInput = new StringBuilder();
            using (var hashAlgorithm = new MD5CryptoServiceProvider())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
                bytes = hashAlgorithm.ComputeHash(bytes);
                foreach (byte b in bytes)
                {
                    hashedInput.Append(b.ToString("x2").ToLower());
                }
            }
            return hashedInput.ToString();
        }

        public static string FormatDate(this HtmlHelper helper, DateViewModel input)
        {
            var formattedDate = string.Format("{0}-{1}-{2}", input.Month, input.Day, input.Year);
            DateTime parsedDateTime;
            if (DateTime.TryParse(formattedDate, out parsedDateTime))
            {
                formattedDate = parsedDateTime.ToShortDateString();
            }
            return formattedDate;
        }
    }
}