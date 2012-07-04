using Typeset.Web.Models.Common;
using Typeset.Web.Models.Configuration;
using Typeset.Web.Models.Posts;

namespace Typeset.Web.Models.Home
{
    public class HomeViewModel
    {
        public ConfigurationViewModel Configuration { get; set; }
        public LayoutViewModel Layout { get; set; }
        public PageOfPostsViewModel Posts { get; set; }

        public HomeViewModel(ConfigurationViewModel configViewModel, LayoutViewModel layoutViewModel, PageOfPostsViewModel pageOfPostViewModel)
        {
            Configuration = configViewModel;
            Layout = layoutViewModel;
            Posts = pageOfPostViewModel;
        }
    }
}