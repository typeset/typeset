using System.Collections.Generic;
using Typeset.Domain.About;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Posts;
using Typeset.Web.Models.Configuration;

namespace Typeset.Web.Models.Home
{
    public class HomeViewModel
    {
        public ConfigurationViewModel Configuration { get; set; }
        public AboutViewModel About { get; set; }
        public PageOfPostsViewModel Posts { get; set; }

        public HomeViewModel(ConfigurationViewModel configViewModel, AboutViewModel aboutViewModel, PageOfPostsViewModel pageOfPostViewModel)
        {
            Configuration = configViewModel;
            About = aboutViewModel;
            Posts = pageOfPostViewModel;
        }
    }
}