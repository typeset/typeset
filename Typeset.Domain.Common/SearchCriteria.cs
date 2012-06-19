using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Common
{
    public class SearchCriteria
    {
        public static SearchCriteria Default = new SearchCriteria(10, 0);

        public int Limit { get; private set; }
        public int Offset { get; private set; }

        public SearchCriteria(int limit, int offset)
        {
            if (limit < 0)
            {
                throw new ArgumentException("limit");
            }

            if (offset < 0)
            {
                throw new ArgumentException("offset");
            }

            Limit = limit;
            Offset = offset;
        }
    }
}
