using System;
using System.Web;
using Typeset.Domain.About;
using Typeset.Web.Models.About;
using Typeset.Web.Models.Posts;

namespace Typeset.Web.Controllers.Api
{
    public class AboutController : BaseApiController
    {
        private IAboutRepository AboutRepository { get; set; }

        public AboutController(IAboutRepository aboutRepository)
        {
            if (aboutRepository == null)
            {
                throw new ArgumentNullException("aboutRepository");
            }

            AboutRepository = aboutRepository;
        }

        public AboutViewModel Get()
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/about.yml");
            var about = AboutRepository.Read(path);
            return new AboutViewModel(about);
        }
    }
}