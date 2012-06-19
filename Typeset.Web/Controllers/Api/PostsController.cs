using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Typeset.Domain.Post;
using System.Web;
using Typeset.Web.Models.Common;
using Typeset.Web.Models.Posts;
using Typeset.Domain.Common;

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

        public PageOfViewModel<PostViewModel, PostSearchCriteriaViewModel> Get(int limit = DefaultLimit, int offset = DefaultOffset)
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/Posts");
            var from = Date.MinValue;
            var to = Date.Today;
            var searchCriteria = new PostSearchCriteria(limit, offset, path, from, to);
            var page = PostRepository.Get(searchCriteria);
            var response = new PageOfViewModel<PostViewModel, PostSearchCriteriaViewModel>()
                {
                    Count = page.Count,
                    Entities = page.Entities.Select(p => new PostViewModel(p)),
                    SearchCriteria = new PostSearchCriteriaViewModel(page.SearchCriteria),
                    TotalCount = page.TotalCount
                };
            return response;
        }
    }
}