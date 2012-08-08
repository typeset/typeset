using System;
using System.Web.Mvc;
using Typeset.Domain.Configuration;
using Typeset.Domain.FrontMatter;
using Typeset.Domain.Layout;
using Typeset.Domain.Markup;
using Typeset.Web.Configuration;
using Typeset.Web.Models.Common;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Home;
using Typeset.Web.Models.Posts;

namespace Typeset.Web.Controllers.Site
{
    public class HomeController : BaseController
    {
        private IConfigurationRepository ConfigRepository { get; set; }
        private IFrontMatterRepository FrontMatterRepository { get; set; }
        private IMarkupProcessorFactory MarkupProcessorFactory { get; set; }

        public HomeController(IConfigurationManager configurationManager,
            IConfigurationRepository configRepository,
            IFrontMatterRepository frontMatterRepository,
            IMarkupProcessorFactory markupProcessorFactory)
            : base(configurationManager)
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

        [OutputCache(Duration = DefaultCacheTime, Location = DefaultOutputCacheLocation, VaryByParam = "limit;offset")]
        public ActionResult Get(int limit = 10, int offset = 0)
        {
            var config = ConfigRepository.Read(ConfigPath);
            var configViewModel = new ConfigurationViewModel(config);

            var searchCriteria = new FrontMatterSearchCriteria(limit, offset, Domain.Common.Order.Descending, PostPath, FrontMatterSearchCriteria.DefaultFrom, FrontMatterSearchCriteria.DefaultTo, string.Empty, true);
            var pageOfPosts = FrontMatterRepository.Get(searchCriteria);
            var pageOfPostViewModel = new PageOfPostsViewModel(pageOfPosts, MarkupProcessorFactory);

            var layoutPath = GetLayoutPath("home");
            var layout = LayoutParser.Parse(layoutPath);
            var layoutViewModel = new LayoutViewModel(layout);

            var homeViewModel = new HomeViewModel(configViewModel, layoutViewModel, pageOfPostViewModel);

            return View(homeViewModel);
        }
    }
}
