using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Common
{
    public enum Order
    {
        Ascending,
        Descending
    }

    public class SearchCriteria
    {
        public const int DefaultLimit = 10;
        public const int DefaultOffset = 0;
        public const Order DefaultOrder = Order.Ascending;
        public static SearchCriteria Default = new SearchCriteria(DefaultLimit, DefaultOffset, DefaultOrder);

        public int Limit { get; private set; }
        public int Offset { get; private set; }
        public Order Order { get; private set; }

        public SearchCriteria(int limit, int offset, Order order)
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
            Order = order;
        }
    }
}
