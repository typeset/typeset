using System;
using Typeset.Domain.FrontMatter;
using Typeset.Web.Models.Common;

namespace Typeset.Web.Models.Posts
{
    public class PostSearchCriteriaViewModel : SearchCriteriaViewModel
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }

        public PostSearchCriteriaViewModel(FrontMatterSearchCriteria entity)
        {
            From = entity.From.Value;
            Limit = entity.Limit;
            Offset = entity.Offset;
            Order = entity.Order.ToString().ToLower();
            To = entity.To.Value;
        }
    }
}