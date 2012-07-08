using System;
using Typeset.Domain.Common;

namespace Typeset.Domain.FrontMatter
{
    public class FrontMatterSearchCriteria : SearchCriteria
    {
        public static DateTimeOffset DefaultFrom { get { return DateTimeOffset.MinValue; } }
        public static DateTimeOffset DefaultTo { get { return DateTimeOffset.UtcNow; } }

        public string Path { get; private set; }
        public DateTimeOffset? From { get; private set; }
        public DateTimeOffset? To { get; private set; }
        public string Permalink { get; private set; }
        public bool Published { get; private set; }

        public FrontMatterSearchCriteria(int limit, int offset, Order order, string path, DateTimeOffset? from, DateTimeOffset? to, string permalink, bool published)
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
