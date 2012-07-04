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

        public DateViewModel(LocalDate? entity)
        {
            if (entity.HasValue)
            {
                Year = entity.Value.Year;
                Month = entity.Value.Month;
                Day = entity.Value.Day;
            }
        }
    }
}