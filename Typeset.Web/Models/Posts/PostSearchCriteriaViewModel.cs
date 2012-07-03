using Typeset.Domain.FrontMatter;
using Typeset.Web.Models.Common;

namespace Typeset.Web.Models.Posts
{
    public class PostSearchCriteriaViewModel : SearchCriteriaViewModel
    {
        public DateViewModel From { get; set; }
        public DateViewModel To { get; set; }

        public PostSearchCriteriaViewModel(FrontMatterSearchCriteria entity)
        {
            From = new DateViewModel(entity.From.Value);
            Limit = entity.Limit;
            Offset = entity.Offset;
            Order = entity.Order.ToString().ToLower();
            To = new DateViewModel(entity.To.Value);
        }
    }
}