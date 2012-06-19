using System.Collections.Generic;
using Typeset.Domain.About;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Posts;

namespace Typeset.Web.Models.Home
{
    public class HomeViewModel
    {
        public AboutViewModel About { get; set; }
        public PageOfPostViewModel Posts { get; set; }

        public HomeViewModel(AboutViewModel aboutViewModel, PageOfPostViewModel pageOfPostViewModel)
        {
            About = aboutViewModel;
            Posts = pageOfPostViewModel;
        }
    }
}