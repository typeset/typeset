using Typeset.Web.Models.Common;

namespace Typeset.Web.Models.Posts
{
    public class PostSearchCriteriaViewModel : SearchCriteriaViewModel
    {
        public DateViewModel From { get; set; }
        public DateViewModel To { get; set; }

        public PostSearchCriteriaViewModel(Domain.Post.PostSearchCriteria entity)
        {
            From = new DateViewModel(entity.From);
            Limit = entity.Limit;
            Offset = entity.Offset;
            Order = entity.Order.ToString().ToLower();
            To = new DateViewModel(entity.To);
        }
    }
}