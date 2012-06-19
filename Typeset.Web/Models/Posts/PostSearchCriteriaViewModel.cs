using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Typeset.Domain.Post;
using Typeset.Web.Models.Common;

namespace Typeset.Web.Models.Posts
{
    public class PostSearchCriteriaViewModel : Typeset.Web.Models.Common.SearchCriteriaViewModel
    {
        public static new PostSearchCriteriaViewModel Default { get { return new PostSearchCriteriaViewModel(); } }

        public DateViewModel From { get; set; }
        public DateViewModel To { get; set; }

        public PostSearchCriteriaViewModel()
            : base()
        {
            From = DateViewModel.MinValue;
            To = DateViewModel.MaxValue;
        }

        public PostSearchCriteriaViewModel(Domain.Post.PostSearchCriteria entity)
        {
            From = new DateViewModel(entity.From);
            Limit = entity.Limit;
            Offset = entity.Offset;
            To = new DateViewModel(entity.To);
        }
    }
}