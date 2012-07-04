using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Layout
{
    internal class Layout : ILayout
    {
        public IEnumerable<string> HeadHtml { get; set; }
        public IEnumerable<string> HeadStyles { get; set; }
        public IEnumerable<string> HeadScripts { get; set; }
        public IEnumerable<string> BodyHtmlPreContent { get; set; }
        public IEnumerable<string> BodyHtmlPostContent { get; set; }
        public IEnumerable<string> BodyScripts { get; set; }

        public Layout()
        {
            HeadHtml = new List<string>();
            HeadStyles = new List<string>();
            HeadScripts = new List<string>();
            BodyHtmlPreContent = new List<string>();
            BodyHtmlPostContent = new List<string>();
            BodyScripts = new List<string>();
        }
    }
}
