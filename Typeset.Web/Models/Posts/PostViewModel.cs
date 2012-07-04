using Typeset.Domain.FrontMatter;
using Typeset.Domain.Markup;
using Typeset.Web.Models.Common;

namespace Typeset.Web.Models.Posts
{
    public class PostViewModel : FrontMatterContentViewModel
    {
        public PostViewModel(IFrontMatter entity, IMarkupProcessorFactory markupProcessorFactory)
            : base(entity, markupProcessorFactory)
        {
        }
    }
}