using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.Configuration;
using Typeset.Domain.Markup;
using Typeset.Domain.Post;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Posts;

namespace Typeset.Web.Controllers.Site
{
    public class PostController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IPostRepository PostRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public PostController(IConfigurationRepository configRepository,
            IPostRepository postRepository,
            IMarkupProcessorFactory markupProcessorFactory)
        {
            if (configRepository == null)
            {
                throw new ArgumentNullException("configRepository");
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
            PostRepository = postRepository;
            MarkupProcessorFactory = markupProcessorFactory;
        }

        public ActionResult Get(string permalink)
        {
            var configPath = HttpContext.Server.MapPath("~/App_Data/config.yml");
            var config = ConfigRepository.Read(configPath);
            var configViewModel = new ConfigurationViewModel(config);

            var postsPath = HttpContext.Server.MapPath("~/App_Data/posts");
            var postSearchCriteria = new PostSearchCriteria(1, 0, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, permalink, true);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            var postViewModel = new PostViewModel(pageOfPost.Entities.First(), MarkupProcessorFactory);

            var pageOfPostViewModel = new PageOfPostViewModel(configViewModel, postViewModel);

            return View(pageOfPostViewModel);
        }
    }
}
