using System;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.Configuration;
using Typeset.Domain.Markup;
using Typeset.Domain.Post;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;
using Typeset.Web.ViewResults;

namespace Typeset.Web.Controllers.Site
{
    public class StaticFileController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IPostRepository PostRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public StaticFileController(IConfigurationRepository configRepository, 
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

        public ActionResult Get(string url)
        {
            try
            {
                var fileStream = System.IO.File.OpenRead(HttpContext.Server.MapPath(string.Format("~/App_Data/content/{0}", url)));
                var contentType = "application/octet-stream";
                try
                {
                    if (url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        url.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "image/jpg";
                    }

                    if(url.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "image/gif";
                    }

                    if (url.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "image/png";
                    }

                    if (url.EndsWith(".ico", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "image/x-icon";
                    }

                    if (url.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "text/css";
                    }

                    if (url.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "text/javascript";
                    }

                    if (url.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
                    {
                        contentType = "text/html";
                    }
                }
                catch { }

                return File(fileStream, contentType);
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

            var postsPath = HttpContext.Server.MapPath("~/App_Data/posts");
            var postSearchCriteria = new PostSearchCriteria(10, 0, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, string.Empty, true);
            var pageOfPosts = PostRepository.Get(postSearchCriteria);
            var pageOfPostsViewModel = new PageOfPostsViewModel(pageOfPosts, MarkupProcessorFactory);

            var homeViewModel = new HomeViewModel(configViewModel, pageOfPostsViewModel);

            return new AtomViewResult(homeViewModel);
        }
    }
}
