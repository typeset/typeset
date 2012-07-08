using Typeset.Domain.FrontMatter;
using Typeset.Domain.Markup;
using Typeset.Web.Models.Common;

namespace Typeset.Web.Models.Posts
{
    public class PostViewModel : ContentViewModel
    {
        public PostViewModel(IFrontMatter entity, IMarkupProcessorFactory markupProcessorFactory)
            : base(entity, markupProcessorFactory)
        {
        }
    }
}