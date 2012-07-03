using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.Common;
using Typeset.Domain.Configuration;
using Typeset.Domain.Markup;
using Typeset.Domain.Pages;
using Typeset.Domain.Post;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;
using Typeset.Web.ViewResults;
using Typeset.Web.Extensions;

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
            var config = ConfigRepository.Read(ConfigPath);
            var configViewModel = new ConfigurationViewModel(config);

            var postSearchCriteria = new PostSearchCriteria(10, 0, Domain.Common.Order.Descending, PostPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, string.Empty, true);
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
            var contentType = url.GetMimeType();
            var fileStream = System.IO.File.OpenRead(string.Format("{0}/{1}", ContentPath, url));
            return File(fileStream, contentType);
        }

        private ActionResult GetPage(string url)
        {
            var searchCriteria = new PageSearchCriteria(1, 0, Domain.Common.Order.Ascending, ContentPath, url, true);
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
            var config = ConfigRepository.Read(ConfigPath);
            var configViewModel = new ConfigurationViewModel(config);

            var postSearchCriteria = new PostSearchCriteria(1, 0, Domain.Common.Order.Descending, PostPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, url, true);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            var postViewModel = new PostViewModel(pageOfPost.Entities.First(), MarkupProcessorFactory);

            var pageOfPostViewModel = new PageOfPostViewModel(configViewModel, postViewModel);

            return View("~/Views/Post/Get.cshtml", pageOfPostViewModel);
        }

        private bool IsPost(string permalink)
        {
            var postSearchCriteria = new PostSearchCriteria(1, 0, Domain.Common.Order.Descending, PostPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, permalink, true);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            return pageOfPost.Entities.Any();

        }

        private bool IsPage(string permalink)
        {
            var searchCriteria = new PageSearchCriteria(1, 0, Domain.Common.Order.Ascending, ContentPath, permalink, true);
            var pageOfPages = PageRepository.Get(searchCriteria);
            return pageOfPages.Entities.Any();
        }
    }
}
