using System.Collections.Generic;
using System.Linq;
using Typeset.Domain.Post;
using Typeset.Domain.Common;
using Typeset.Web.Models.Common;

namespace Typeset.Web.Models.Posts
{
    public class PageOfPostViewModel : PageOfViewModel<PostViewModel, PostSearchCriteriaViewModel>
    {
        public PageOfPostViewModel()
            : base()
        {
            
        }

        public PageOfPostViewModel(PageOf<IPost, PostSearchCriteria> entity)
        {
            this.Count = entity.Count;
            this.Entities = entity.Entities.Select(e => new PostViewModel(e));
            this.SearchCriteria = new PostSearchCriteriaViewModel(entity.SearchCriteria);
            this.TotalCount = entity.TotalCount;
        }
    }
}