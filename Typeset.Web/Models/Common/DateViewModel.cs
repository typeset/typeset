using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NodaTime;

namespace Typeset.Web.Models.Common
{
    public class DateViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public DateViewModel(LocalDate entity)
        {
            Year = entity.Year;
            Month = entity.Month;
            Day = entity.Day;
        }
    }
}