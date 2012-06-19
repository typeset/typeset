using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Post
{
    public class PostSearchCriteria : SearchCriteria
    {
        public string Path { get; private set; }
        public Date From { get; private set; }
        public Date To { get; private set; }

        public PostSearchCriteria(int limit, int offset, string path, Date from, Date to)
            : base(limit, offset)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            if (from == null)
            {
                throw new ArgumentNullException("fromDate");
            }

            if (to == null)
            {
                throw new ArgumentNullException("toDate");
            }

            Path = path;
            From = from;
            To = to;
        }
    }
}
