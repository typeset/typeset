using System;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.About;
using Typeset.Domain.Markup;
using Typeset.Domain.Post;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;
using System.Collections;
using System.Collections.Generic;

namespace Typeset.Web.Controllers.Site
{
    public class HomeController : BaseController
    {
        private IAboutRepository AboutRepository { get; set; }
        private IPostRepository PostRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public HomeController(IAboutRepository aboutRepository,
            IPostRepository postRepository,
            IMarkupProcessorFactory markupProcessorFactory)
        {
            if (aboutRepository == null)
            {
                throw new ArgumentNullException("aboutRepository");
            }

            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }

            if (markupProcessorFactory == null)
            {
                throw new ArgumentNullException("markupProcessorFactory");
            }

            AboutRepository = aboutRepository;
            PostRepository = postRepository;
            MarkupProcessorFactory = markupProcessorFactory;
        }

        public ActionResult Index(int limit = 10, int offset = 0)
        {
            var aboutPath = HttpContext.Server.MapPath("~/App_Data/about.yml");
            var about = AboutRepository.Read(aboutPath);
            var aboutViewModel = new AboutViewModel(about);

            var postsPath = HttpContext.Server.MapPath("~/App_Data/posts");
            var postSearchCriteria = new PostSearchCriteria(limit, offset, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            var pageOfPostViewModel = new PageOfPostViewModel(pageOfPost, MarkupProcessorFactory);

            var homeViewModel = new HomeViewModel(aboutViewModel, pageOfPostViewModel);

            return View(homeViewModel);
        }
    }
}
