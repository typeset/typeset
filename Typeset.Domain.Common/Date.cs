using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Common
{
    public class Date
    {
        public static Date MinValue { get { return  new Date(1, 1, 1); } }
        public static Date MaxValue { get { return new Date(int.MaxValue, 12, 31); } }
        public static Date Today { get { return new Date(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, DateTimeOffset.UtcNow.Day); } }

        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }

        public Date(int year, int month, int day)
        {
            if (year < 1)
            {
                throw new ArgumentException("Invalid year");
            }

            if (month < 1 || month > 12)
            {
                throw new ArgumentException("Invalid month");
            }

            if (day < 0 || day > 31)
            {
                throw new ArgumentException("Invalid day");
            }

            try
            {
                new DateTime(year, month, day);
            }
            catch 
            {
                throw new ArgumentException("Invalid date");
            }

            Year = year;
            Month = month;
            Day = day;
        }

        public static bool operator <(Date date1, Date date2)
        {
            return new DateTime(date1.Year, date1.Month, date1.Day) < new DateTime(date2.Year, date2.Month, date2.Day);
        }

        public static bool operator <=(Date date1, Date date2)
        {
            return new DateTime(date1.Year, date1.Month, date1.Day) <= new DateTime(date2.Year, date2.Month, date2.Day);
        }

        public static bool operator >(Date date1, Date date2)
        {
            return new DateTime(date1.Year, date1.Month, date1.Day) > new DateTime(date2.Year, date2.Month, date2.Day);
        }

        public static bool operator >=(Date date1, Date date2)
        {
            return new DateTime(date1.Year, date1.Month, date1.Day) >= new DateTime(date2.Year, date2.Month, date2.Day);
        }
    }
}
