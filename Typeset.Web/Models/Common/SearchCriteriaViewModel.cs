using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Typeset.Domain.Post;

namespace Typeset.Web.Models.Common
{
    public class SearchCriteriaViewModel
    {
        public static SearchCriteriaViewModel Default { get { return new SearchCriteriaViewModel(); } }

        public int Limit { get; set; }
        public int Offset { get; set; }

        public SearchCriteriaViewModel()
        {
            Limit = 10;
            Offset = 0;
        }
    }
}