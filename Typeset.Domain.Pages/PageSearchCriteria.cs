using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Pages
{
    public class PageSearchCriteria : SearchCriteria
    {
        public string Path { get; private set; }
        public string Permalink { get; private set; }
        public bool Published { get; private set; }

        public PageSearchCriteria(int limit, int offset, Order order, string path, string permalink, bool published)
            : base(limit, offset, order)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            Path = path;
            Permalink = string.IsNullOrWhiteSpace(permalink) ? string.Empty : permalink;
            Published = published;
        }
    }
}
