using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;
using NodaTime;

namespace Typeset.Domain.FrontMatter
{
    public class FrontMatterSearchCriteria : SearchCriteria
    {
        public static LocalDate MinDate { get { return new LocalDate(0, 1, 1); } }
        public static LocalDate MaxDate { get { return new LocalDate(3000, 12, 31); } }
        public static LocalDate DefaultFrom { get { return MinDate; } }
        public static LocalDate DefaultTo { get { return new LocalDate(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, DateTimeOffset.UtcNow.Day); } }

        public string Path { get; private set; }
        public LocalDate? From { get; private set; }
        public LocalDate? To { get; private set; }
        public string Permalink { get; private set; }
        public bool Published { get; private set; }

        public FrontMatterSearchCriteria(int limit, int offset, Order order, string path, LocalDate? from, LocalDate? to, string permalink, bool published)
            : base(limit, offset, order)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            Path = path;
            From = from;
            To = to;
            Permalink = string.IsNullOrWhiteSpace(permalink) ? string.Empty : permalink;
            Published = published;
        }
    }
}
