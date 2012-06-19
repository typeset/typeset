using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.Post;
using Typeset.Domain.About;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Posts;
using Typeset.Web.Models.Home;

namespace Typeset.Web.Controllers.Site
{
    public class HomeController : BaseController
    {
        private IAboutRepository AboutRepository { get; set; }
        private IPostRepository PostRepository { get; set; }

        public HomeController(IAboutRepository aboutRepository, IPostRepository postRepository)
        {
            if (aboutRepository == null)
            {
                throw new ArgumentNullException("aboutRepository");
            }

            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }

            AboutRepository = aboutRepository;
            PostRepository = postRepository;
        }

        public ActionResult Index()
        {
            var aboutPath = HttpContext.Server.MapPath("~/App_Data/about.yml");
            var about = AboutRepository.Read(aboutPath);
            var aboutViewModel = new AboutViewModel(about);

            var postsPath = HttpContext.Server.MapPath("~/App_Data/posts");
            var postSearchCriteria = new PostSearchCriteria(10, 0, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo);
            var posts = PostRepository.Get(postSearchCriteria);
            var pageOfPostViewModel = new PageOfPostViewModel(posts);

            var homeViewModel = new HomeViewModel(aboutViewModel, pageOfPostViewModel);

            return View(homeViewModel);
        }
    }
}
