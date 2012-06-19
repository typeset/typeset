using System.Collections.Generic;

namespace Typeset.Web.Models.Common
{
    public class PageOfViewModel<TEntities, TSearchCriteria>
        where TSearchCriteria : SearchCriteriaViewModel
    {
        public IEnumerable<TEntities> Entities { get; set; }
        public TSearchCriteria SearchCriteria { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }

        public PageOfViewModel()
        {
            SearchCriteria = default(TSearchCriteria);
            Entities = new List<TEntities>();
        }
    }
}