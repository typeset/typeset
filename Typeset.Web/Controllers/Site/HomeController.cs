﻿using System;
using System.Web.Mvc;
using Typeset.Domain.Configuration;
using Typeset.Domain.Markup;
using Typeset.Domain.Post;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;

namespace Typeset.Web.Controllers.Site
{
    public class HomeController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IPostRepository PostRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public HomeController(IConfigurationRepository configRepository,
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

        public ActionResult Get(int limit = 10, int offset = 0)
        {
            var config = ConfigRepository.Read(ConfigPath);
            var configViewModel = new ConfigurationViewModel(config);

            var postSearchCriteria = new PostSearchCriteria(limit, offset, Domain.Common.Order.Descending, PostPath, PostSearchCriteria.DefaultFrom, PostSearchCriteria.DefaultTo, string.Empty, true);
            var pageOfPost = PostRepository.Get(postSearchCriteria);
            var pageOfPostViewModel = new PageOfPostsViewModel(pageOfPost, MarkupProcessorFactory);

            var homeViewModel = new HomeViewModel(configViewModel, pageOfPostViewModel);

            return View(homeViewModel);
        }
    }
}
