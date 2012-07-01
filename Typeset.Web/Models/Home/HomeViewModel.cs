using System.Collections.Generic;
using Typeset.Web.Models.Posts;
using Typeset.Web.Models.Configuration;

namespace Typeset.Web.Models.Home
{
    public class HomeViewModel
    {
        public ConfigurationViewModel Configuration { get; set; }
        public PageOfPostsViewModel Posts { get; set; }

        public HomeViewModel(ConfigurationViewModel configViewModel, PageOfPostsViewModel pageOfPostViewModel)
        {
            Configuration = configViewModel;
            Posts = pageOfPostViewModel;
        }
    }
}