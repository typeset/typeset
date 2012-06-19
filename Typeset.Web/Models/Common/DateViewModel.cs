using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Typeset.Domain.Post;

namespace Typeset.Web.Models.Common
{
    public class DateViewModel
    {
        public static DateViewModel MinValue { get { return new DateViewModel(Domain.Common.Date.MinValue); } }
        public static DateViewModel MaxValue { get { return new DateViewModel(Domain.Common.Date.MaxValue); } }
        public static DateViewModel Today { get { return new DateViewModel(Domain.Common.Date.Today); } }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public DateViewModel()
            : this(Domain.Common.Date.MinValue)
        {
        }

        public DateViewModel(Typeset.Domain.Common.Date entity)
        {
            Year = entity.Year;
            Month = entity.Month;
            Day = entity.Day;
        }
    }
}