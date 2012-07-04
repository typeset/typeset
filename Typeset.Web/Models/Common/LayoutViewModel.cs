using System.Collections.Generic;
using Typeset.Domain.Layout;

namespace Typeset.Web.Models.Common
{
    public class LayoutViewModel
    {
        public IEnumerable<string> HeadHtml { get; private set; }
        public IEnumerable<string> HeadStyles { get; private set; }
        public IEnumerable<string> HeadScripts { get; private set; }
        public IEnumerable<string> BodyHtmlPreContent { get; private set; }
        public IEnumerable<string> BodyHtmlPostContent { get; private set; }
        public IEnumerable<string> BodyScripts { get; private set; }

        public LayoutViewModel()
        {
            HeadHtml = new List<string>();
            HeadStyles = new List<string>();
            HeadScripts = new List<string>();
            BodyHtmlPreContent = new List<string>();
            BodyHtmlPostContent = new List<string>();
            BodyScripts = new List<string>();
        }

        public LayoutViewModel(ILayout entity)
        {
            HeadHtml = entity.HeadHtml;
            HeadStyles = entity.HeadStyles;
            HeadScripts = entity.HeadScripts;
            BodyHtmlPreContent = entity.BodyHtmlPreContent;
            BodyHtmlPostContent = entity.BodyHtmlPostContent;
            BodyScripts = entity.BodyScripts;
        }
    }
}