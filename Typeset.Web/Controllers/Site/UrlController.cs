using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.Configuration;
using Typeset.Domain.Markup;
using Typeset.Domain.Pages;
using Typeset.Domain.Post;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;
using Typeset.Web.ViewResults;
using Typeset.Domain.Common;

namespace Typeset.Web.Controllers.Site
{
    public class UrlController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IPostRepository PostRepository { get; set; }
        private IPageRepository PageRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public UrlController(IConfigurationRepository configRepository, 
            IPostRepository postRepository,
            IPageRepository pageRepository,
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

            if (pageRepository == null)
            {
                throw new ArgumentNullException("pageRepository");
            }

            if (markupProcessorFactory == null)
            {
                throw new ArgumentNullException("markupProcessorFactory");
            }

            ConfigRepository = configRepository;
            PostRepository = postRepository;
            PageRepository = pageRepository;
            MarkupProcessorFactory = markupProcessorFactory;
        }

        public ActionResult Atom()
        {
            var configPath = HttpContext.Server.MapPath("~/App_Data/_config.yml");
            var config = ConfigRepository.Read(configPath);
            var configViewModel = new ConfigurationViewModel(config);

            var postsPath = HttpContext.Server.MapPath("~/App_Data/_posts");
            var postSearchCriteria = new PostSearchCriteria(10, 0, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, string.Empty, true);
            var pageOfPosts = PostRepository.Get(postSearchCriteria);
            var pageOfPostsViewModel = new PageOfPostsViewModel(pageOfPosts, MarkupProcessorFactory);

            var homeViewModel = new HomeViewModel(configViewModel, pageOfPostsViewModel);

            return new AtomViewResult(homeViewModel);
        }

        public ActionResult Get(string url)
        {
            try
            {
                if (IsPost(url))
                {
                    return GetPost(url);
                }
                else if (IsPage(url))
                {
                    return GetPage(url);
                }
                else
                {
                    return GetFile(url);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(404);
            }
        }

        private ActionResult GetFile(string url)
        {
            var fileStream = System.IO.File.OpenRead(HttpContext.Server.MapPath(string.Format("~/App_Data/{0}", url)));
            var contentType = "application/octet-stream";
            try
            {
                if (url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    url.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    contentType = "image/jpg";
                }

                if (url.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
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

                if (FrontMatter.FrontMatterExtensions.Any(ext => url.EndsWith(ext, StringComparison.OrdinalIgnoreCase)) ||
                    url.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    contentType = "text/plain";
                }
            }
            catch { }

            return File(fileStream, contentType);
        }

        private ActionResult GetPage(string url)
        {
            var path = HttpContext.Server.MapPath("~/App_Data");
            var searchCriteria = new PageSearchCriteria(1, 0, Domain.Common.Order.Ascending, path, url, true);
            var pageOfPages = PageRepository.Get(searchCriteria);
            var entity = pageOfPages.Entities.First();
            var content = MarkupProcessorFactory.CreateInstance(entity.ContentType).Process(entity.Content);
            return new ContentResult()
            {
                Content = content,
                ContentEncoding = Encoding.UTF8,
                ContentType = "text/html"
            };
        }

        private ActionResult GetPost(string url)
        {
            var configPath = HttpContext.Server.MapPath("~/App_Data/_config.yml");
            var config = ConfigRepository.Read(configPath);
            var configViewModel = new ConfigurationViewModel(config);

            var postsPath = HttpContext.Server.MapPath("~/App_Data/_posts");
            var postSearchCriteria = new PostSearchCriteria(1, 0, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, url, true);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            var postViewModel = new PostViewModel(pageOfPost.Entities.First(), MarkupProcessorFactory);

            var pageOfPostViewModel = new PageOfPostViewModel(configViewModel, postViewModel);

            return View("~/Views/Post/Get.cshtml", pageOfPostViewModel);
        }

        private bool IsPost(string permalink)
        {
            var postsPath = HttpContext.Server.MapPath("~/App_Data/_posts");
            var postSearchCriteria = new PostSearchCriteria(1, 0, Domain.Common.Order.Descending, postsPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, permalink, true);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            return pageOfPost.Entities.Any();

        }

        private bool IsPage(string permalink)
        {
            var path = HttpContext.Server.MapPath("~/App_Data");
            var searchCriteria = new PageSearchCriteria(1, 0, Domain.Common.Order.Ascending, path, permalink, true);
            var pageOfPages = PageRepository.Get(searchCriteria);
            return pageOfPages.Entities.Any();
        }
    }
}
