using System.Collections.Generic;
using System.Linq;
using Typeset.Domain.Common;
using Typeset.Domain.Post;
using Typeset.Domain.Markup;

namespace Typeset.Web.Models.Posts
{
    public class PageOfPostsViewModel
    {
        public IEnumerable<PostViewModel> Entities { get; set; }
        public PostSearchCriteriaViewModel SearchCriteria { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }

        public PageOfPostsViewModel(PageOf<IPost, PostSearchCriteria> entity, IMarkupProcessorFactory markupProcessorFactory)
        {
            Count = entity.Count;
            Entities = entity.Entities.Select(e => new PostViewModel(e, markupProcessorFactory)).ToList();
            SearchCriteria = new PostSearchCriteriaViewModel(entity.SearchCriteria);
            TotalCount = entity.TotalCount;
        }
    }
}