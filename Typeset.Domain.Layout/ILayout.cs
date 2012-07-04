using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Layout
{
    public interface ILayout
    {
        IEnumerable<string> HeadHtml { get; }
        IEnumerable<string> HeadStyles { get; }
        IEnumerable<string> HeadScripts { get; }
        IEnumerable<string> BodyHtmlPreContent { get; }
        IEnumerable<string> BodyHtmlPostContent { get; }
        IEnumerable<string> BodyScripts { get; }
    }
}
