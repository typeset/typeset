using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Post
{
    public class SearchCriteria
    {
        public int? Year { get; private set; }
        public int? Month { get; private set; }
        public int? Day { get; private set; }

        public SearchCriteria(int? year, int? month, int? day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
    }
}
