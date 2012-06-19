using System;
using System.Web;
using Typeset.Domain.Common;
using Typeset.Domain.Post;
using Typeset.Web.Models.Posts;
using NodaTime;

namespace Typeset.Web.Controllers.Api
{
    public class PostsController : BaseApiController
    {
        private IPostRepository PostRepository { get; set; }

        public PostsController(IPostRepository postRepository)
        {
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }

            PostRepository = postRepository;
        }

        public PageOfPostViewModel Get(int limit = SearchCriteria.DefaultLimit, int offset = SearchCriteria.DefaultOffset, string order = "descending")
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/Posts");
            var from = PostSearchCriteria.DefaultFrom;
            var to = PostSearchCriteria.DefaultTo;
            var orderParsed = SearchCriteria.DefaultOrder;
            Enum.TryParse<Order>(order, true, out orderParsed);
            var searchCriteria = new PostSearchCriteria(limit, offset, orderParsed, path, from, to);
            var page = PostRepository.Get(searchCriteria);
            return new PageOfPostViewModel(page);
        }
    }
}