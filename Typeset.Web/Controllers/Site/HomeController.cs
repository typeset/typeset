using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Typeset.Domain.Post;

namespace Typeset.Web.Controllers.Site
{
    public class HomeController : BaseController
    {
        private IPostRepository PostRepository { get; set; }

        public HomeController(IPostRepository postRepository)
        {
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }

            PostRepository = postRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
