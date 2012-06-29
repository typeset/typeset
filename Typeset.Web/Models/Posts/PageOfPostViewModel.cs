using Typeset.Web.Models.About;
using Typeset.Web.Models.Configuration;

namespace Typeset.Web.Models.Posts
{
    public class PageOfPostViewModel
    {
        public ConfigurationViewModel Configuration { get; set; }
        public AboutViewModel About { get; set; }
        public PostViewModel Post { get; set; }

        public PageOfPostViewModel(ConfigurationViewModel configViewModel, AboutViewModel aboutViewModel, PostViewModel postViewModel)
        {
            Configuration = configViewModel;
            About = aboutViewModel;
            Post = postViewModel;
        }
    }
}