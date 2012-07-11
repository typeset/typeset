using System;
using Typeset.Domain.Common;
using Typeset.Domain.FrontMatter;
using Typeset.Domain.Markup;
using Typeset.Web.Configuration;
using Typeset.Web.Models.Posts;

namespace Typeset.Web.Controllers.Api
{
    public class PostsController : BaseApiController
    {
        private IFrontMatterRepository FrontMatterRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public PostsController(IConfigurationManager configurationManager,
            IFrontMatterRepository frontMatterRepository,
            IMarkupProcessorFactory markupProcessorFactory)
            : base(configurationManager)
        {
            if (frontMatterRepository == null)
            {
                throw new ArgumentNullException("frontMatterRepository");
            }

            if (markupProcessorFactory == null)
            {
                throw new ArgumentNullException("markupProcessorFactory");
            }

            FrontMatterRepository = frontMatterRepository;
            MarkupProcessorFactory = markupProcessorFactory;
        }

        public PageOfPostsViewModel Get(int limit = SearchCriteria.DefaultLimit, int offset = SearchCriteria.DefaultOffset, string order = "descending")
        {
            var from = FrontMatterSearchCriteria.DefaultFrom;
            var to = FrontMatterSearchCriteria.DefaultTo;
            var orderParsed = SearchCriteria.DefaultOrder;
            Enum.TryParse<Order>(order, true, out orderParsed);
            var searchCriteria = new FrontMatterSearchCriteria(limit, offset, orderParsed, PostPath, from, to, string.Empty, true);
            var pageOfPost = FrontMatterRepository.Get(searchCriteria);
            
            var pageOfPostViewModel = new PageOfPostsViewModel(pageOfPost, MarkupProcessorFactory);

            return pageOfPostViewModel;
        }
    }
}