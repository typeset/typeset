using Typeset.Domain.About;
using Typeset.Domain.Markup;
using Typeset.Domain.Post;
using Typeset.Web.Models.About;

namespace Typeset.Web.Models.Posts
{
    public class PageOfPostViewModel
    {
        public AboutViewModel About { get; set; }
        public PostViewModel Post { get; set; }

        public PageOfPostViewModel(AboutViewModel aboutViewModel, PostViewModel postViewModel)
        {
            About = aboutViewModel;
            Post = postViewModel;
        }
    }
}