using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Common
{
    public class PageOf<TEntities, TSearchCriteria>
        where TSearchCriteria : SearchCriteria
    {
        public TSearchCriteria SearchCriteria { get; private set; }
        public IEnumerable<TEntities> Entities { get; private set; }
        public int Count { get; private set; }
        public int TotalCount { get; private set; }

        public PageOf(TSearchCriteria searchCriteria, IEnumerable<TEntities> entities, int totalCount)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            if (totalCount < 0)
            {
                throw new ArgumentException("totalCount");
            }

            SearchCriteria = searchCriteria;
            Entities = entities;
            Count = entities.Count();
            TotalCount = totalCount;
        }
    }
}
