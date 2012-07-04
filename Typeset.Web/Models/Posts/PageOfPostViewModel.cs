using Typeset.Web.Models.Common;
using Typeset.Web.Models.Configuration;

namespace Typeset.Web.Models.Posts
{
    public class PageOfPostViewModel
    {
        public ConfigurationViewModel Configuration { get; set; }
        public LayoutViewModel Layout { get; set; }
        public PostViewModel Post { get; set; }

        public PageOfPostViewModel(ConfigurationViewModel configViewModel, LayoutViewModel layoutViewModel, PostViewModel postViewModel)
        {
            Configuration = configViewModel;
            Layout = layoutViewModel;
            Post = postViewModel;
        }
    }
}