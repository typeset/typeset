using System;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.About;
using Typeset.Domain.Configuration;
using Typeset.Domain.Markup;
using Typeset.Domain.Post;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;
using Typeset.Web.ViewResults;

namespace Typeset.Web.Controllers.Site
{
    public class StaticFileController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IAboutRepository AboutRepository { get; set; }
        private IPostRepository PostRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public StaticFileController(IConfigurationRepository configRepository, 
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

        public ActionResult Get(string url)
        {
            try
            {
                var fileStream = System.IO.File.OpenRead(HttpContext.Server.MapPath(string.Format("~/App_Data/content/{0}", url)));
                return File(fileStream, "application/octet-stream");
            }
            catch
            {
                return new HttpStatusCodeResult(404);
            }
        }

        public ActionResult Atom()
        {
            var configPath = HttpContext.Server.MapPath("~/App_Data/config.yml");
            var config = ConfigRepository.Read(configPath);
            var configViewModel = new ConfigurationViewModel(config);

            var aboutPath = HttpContext.Server.MapPath("~/App_Data/about.yml");
            var about = AboutRepository.Read(aboutPath);
            var aboutViewModel = new AboutViewModel(about);

            var postsPath = HttpContext.Server.MapPath("~/App_Data/posts");
            var postSearchCriteria = new PostSearchCriteria(10, 0, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, string.Empty, true);
            var pageOfPosts = PostRepository.Get(postSearchCriteria);
            var pageOfPostsViewModel = new PageOfPostsViewModel(pageOfPosts, MarkupProcessorFactory);

            var homeViewModel = new HomeViewModel(configViewModel, aboutViewModel, pageOfPostsViewModel);

            return new AtomViewResult(homeViewModel);
        }
    }
}
