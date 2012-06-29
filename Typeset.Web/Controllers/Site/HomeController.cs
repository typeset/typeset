using System;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.About;
using Typeset.Domain.Configuration;
using Typeset.Domain.Markup;
using Typeset.Domain.Post;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;
using Typeset.Web.Models.Configuration;

namespace Typeset.Web.Controllers.Site
{
    public class HomeController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IAboutRepository AboutRepository { get; set; }
        private IPostRepository PostRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public HomeController(IConfigurationRepository configRepository,
            IAboutRepository aboutRepository,
            IPostRepository postRepository,
            IMarkupProcessorFactory markupProcessorFactory)
        {
            if (configRepository == null)
            {
                throw new ArgumentNullException("configRepository");
            }

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

            ConfigRepository = configRepository;
            AboutRepository = aboutRepository;
            PostRepository = postRepository;
            MarkupProcessorFactory = markupProcessorFactory;
        }

        public ActionResult Get(int limit = 10, int offset = 0)
        {
            var configPath = HttpContext.Server.MapPath("~/App_Data/config.yml");
            var config = ConfigRepository.Read(configPath);
            var configViewModel = new ConfigurationViewModel(config);

            var aboutPath = HttpContext.Server.MapPath("~/App_Data/about.yml");
            var about = AboutRepository.Read(aboutPath);
            var aboutViewModel = new AboutViewModel(about);

            var postsPath = HttpContext.Server.MapPath("~/App_Data/posts");
            var postSearchCriteria = new PostSearchCriteria(limit, offset, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, string.Empty, true);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            var pageOfPostViewModel = new PageOfPostsViewModel(pageOfPost, MarkupProcessorFactory);

            var homeViewModel = new HomeViewModel(configViewModel, aboutViewModel, pageOfPostViewModel);

            return View(homeViewModel);
        }
    }
}
