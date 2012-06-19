using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Typeset.Web.Models.Common
{
    public class SearchCriteriaViewModel
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Order { get; set; }
    }
}