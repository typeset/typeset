﻿using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Typeset.Domain.Configuration;
using Typeset.Domain.FrontMatter;
using Typeset.Domain.Markup;
using Typeset.Web.Extensions;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;
using Typeset.Web.ViewResults;
using System.IO;
using Typeset.Domain.Common;
using NodaTime;

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

            var postSearchCriteria = new FrontMatterSearchCriteria(10, 0, Domain.Common.Order.Descending, PostPath, FrontMatterSearchCriteria.DefaultFrom, FrontMatterSearchCriteria.DefaultTo, string.Empty, true);
            var pageOfPosts = FrontMatterRepository.Get(postSearchCriteria);
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
            var path = Path.Combine(ContentPath, url);
            var fileStream = System.IO.File.OpenRead(path);
            return File(fileStream, contentType);
        }

        private ActionResult GetPage(string url)
        {
            var searchCriteria = new FrontMatterSearchCriteria(1, 0, Order.Ascending, ContentPath, null, null, url, true);
            var pageOfPages = FrontMatterRepository.Get(searchCriteria);
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

            var postSearchCriteria = new FrontMatterSearchCriteria(1, 0, Domain.Common.Order.Descending, PostPath, FrontMatterSearchCriteria.DefaultFrom, FrontMatterSearchCriteria.DefaultTo, url, true);
            var pageOfPost = FrontMatterRepository.Get(postSearchCriteria);
            var postViewModel = new PostViewModel(pageOfPost.Entities.First(), MarkupProcessorFactory);

            var pageOfPostViewModel = new PageOfPostViewModel(configViewModel, postViewModel);

            return View("~/Views/Post/Get.cshtml", pageOfPostViewModel);
        }

        private bool IsPost(string url)
        {
            var searchCriteria = new FrontMatterSearchCriteria(1, 0, Domain.Common.Order.Descending, PostPath, FrontMatterSearchCriteria.DefaultFrom, FrontMatterSearchCriteria.DefaultTo, url, true);
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
