using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.About;
using Typeset.Domain.Configuration;
using Typeset.Domain.Markup;
using Typeset.Domain.Post;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Posts;

namespace Typeset.Web.Controllers.Site
{
    public class PostController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IAboutRepository AboutRepository { get; set; }
        private IPostRepository PostRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public PostController(IConfigurationRepository configRepository,
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

        public ActionResult Get(string permalink)
        {
            var configPath = HttpContext.Server.MapPath("~/App_Data/config.yml");
            var config = ConfigRepository.Read(configPath);
            var configViewModel = new ConfigurationViewModel(config);

            var aboutPath = HttpContext.Server.MapPath("~/App_Data/about.yml");
            var about = AboutRepository.Read(aboutPath);
            var aboutViewModel = new AboutViewModel(about);

            var postsPath = HttpContext.Server.MapPath("~/App_Data/posts");
            var postSearchCriteria = new PostSearchCriteria(1, 0, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, permalink, true);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            var postViewModel = new PostViewModel(pageOfPost.Entities.First(), MarkupProcessorFactory);

            var pageOfPostViewModel = new PageOfPostViewModel(configViewModel, aboutViewModel, postViewModel);

            return View(pageOfPostViewModel);
        }
    }
}
