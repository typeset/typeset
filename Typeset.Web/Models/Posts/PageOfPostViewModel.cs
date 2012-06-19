using System.Collections.Generic;
using System.Linq;
using Typeset.Domain.Common;
using Typeset.Domain.Post;

namespace Typeset.Web.Models.Posts
{
    public class PageOfPostViewModel
    {
        public IEnumerable<PostViewModel> Entities { get; set; }
        public PostSearchCriteriaViewModel SearchCriteria { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }

        public PageOfPostViewModel(PageOf<IPost, PostSearchCriteria> entity)
        {
            Count = entity.Count;
            Entities = entity.Entities.Select(e => new PostViewModel(e)).ToList();
            SearchCriteria = new PostSearchCriteriaViewModel(entity.SearchCriteria);
            TotalCount = entity.TotalCount;
        }
    }
}