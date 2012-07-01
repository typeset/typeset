using Typeset.Web.Models.Configuration;

namespace Typeset.Web.Models.Posts
{
    public class PageOfPostViewModel
    {
        public ConfigurationViewModel Configuration { get; set; }
        public PostViewModel Post { get; set; }

        public PageOfPostViewModel(ConfigurationViewModel configViewModel, PostViewModel postViewModel)
        {
            Configuration = configViewModel;
            Post = postViewModel;
        }
    }
}