using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Typeset.Domain.Common;
using Typeset.Domain.Configuration;
using Typeset.Domain.FrontMatter;
using Typeset.Domain.Layout;
using Typeset.Domain.Markup;
using Typeset.Web.Extensions;
using Typeset.Web.Models.Common;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;
using Typeset.Web.ViewResults;

namespace Typeset.Web.Controllers.Site
{
    public class UrlController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IFrontMatterRepository FrontMatterRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public UrlController(IConfigurationRepository configRepository, 
            IFrontMatterRepository frontMatterRepository,
            IMarkupProcessorFactory markupProcessorFactory)
        {
            if (configRepository == null)
            {
                throw new ArgumentNullException("configRepository");
            }

            if (frontMatterRepository == null)
            {
                throw new ArgumentNullException("frontMatterRepository");
            }

            if (markupProcessorFactory == null)
            {
                throw new ArgumentNullException("markupProcessorFactory");
            }

            ConfigRepository = configRepository;
            FrontMatterRepository = frontMatterRepository;
            MarkupProcessorFactory = markupProcessorFactory;
        }

        public ActionResult Atom()
        {
            var config = ConfigRepository.Read(ConfigPath);
            var configViewModel = new ConfigurationViewModel(config);

            var postSearchCriteria = new FrontMatterSearchCriteria(10, 0, Order.Descending, PostPath, FrontMatterSearchCriteria.DefaultFrom, FrontMatterSearchCriteria.DefaultTo, string.Empty, true);
            var pageOfPosts = FrontMatterRepository.Get(postSearchCriteria);
            var pageOfPostsViewModel = new PageOfPostsViewModel(pageOfPosts, MarkupProcessorFactory);

            var layoutViewModel = new LayoutViewModel();

            var homeViewModel = new HomeViewModel(configViewModel, layoutViewModel, pageOfPostsViewModel);

            return new AtomViewResult("~/Views/StaticFile/Atom.cshtml", homeViewModel);
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

        private ActionResult GetPost(string url)
        {
            var config = ConfigRepository.Read(ConfigPath);
            var configViewModel = new ConfigurationViewModel(config);

            var postSearchCriteria = new FrontMatterSearchCriteria(1, 0, Order.Descending, PostPath, FrontMatterSearchCriteria.DefaultFrom, FrontMatterSearchCriteria.DefaultTo, url, true);
            var pageOfPost = FrontMatterRepository.Get(postSearchCriteria);
            var post = pageOfPost.Entities.First();
            var postViewModel = new PostViewModel(post, MarkupProcessorFactory);

            var layoutPath = GetLayoutPath(post.Layout);
            var layout = LayoutParser.Parse(layoutPath);
            var layoutViewModel = new LayoutViewModel(layout);

            var pageOfPostViewModel = new PageOfPostViewModel(configViewModel, layoutViewModel, postViewModel);

            return View("~/Views/Post/Get.cshtml", pageOfPostViewModel);
        }

        private ActionResult GetPage(string url)
        {
            var config = ConfigRepository.Read(ConfigPath);
            var configViewModel = new ConfigurationViewModel(config);

            var searchCriteria = new FrontMatterSearchCriteria(1, 0, Order.Ascending, ContentPath, null, null, url, true);
            var pageOfPages = FrontMatterRepository.Get(searchCriteria);
            var frontMatter = pageOfPages.Entities.First();
            var frontMatterContentViewModel = new FrontMatterContentViewModel(frontMatter, MarkupProcessorFactory);

            var layoutPath = GetLayoutPath(frontMatter.Layout);
            var layout = LayoutParser.Parse(layoutPath);
            var layoutViewModel = new LayoutViewModel(layout);

            var pageOfFrontMatterContentViewModel = new PageOfFrontMatterContentViewModel(configViewModel, layoutViewModel, frontMatterContentViewModel);

            return View("~/Views/StaticFile/Default.cshtml", pageOfFrontMatterContentViewModel);
        }

        private ActionResult GetFile(string url)
        {
            var path = Path.Combine(ContentPath, url);

            if (url.StartsWith("_") ||
                url.Contains("/_") ||
                FrontMatterParser.Yaml.HasFrontMatter(path))
            {
                return new HttpStatusCodeResult(404);
            }
            else
            {
                var contentType = url.GetMimeType();
                var fileStream = System.IO.File.OpenRead(path);
                return File(fileStream, contentType);
            }
        }

        private bool IsPost(string url)
        {
            var searchCriteria = new FrontMatterSearchCriteria(1, 0, Order.Descending, PostPath, FrontMatterSearchCriteria.DefaultFrom, FrontMatterSearchCriteria.DefaultTo, url, true);
            var pageOf = FrontMatterRepository.Get(searchCriteria);
            return pageOf.Entities.Any();
        }

        private bool IsPage(string url)
        {
            var searchCriteria = new FrontMatterSearchCriteria(1, 0, Order.Ascending, ContentPath, null, null, url, true);
            var pageOf = FrontMatterRepository.Get(searchCriteria);
            return pageOf.Entities.Any();
        }
    }
}
