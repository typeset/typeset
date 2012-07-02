using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Pages
{
    public interface IPage
    {
        string Title { get; }
        string Content { get; }
        ContentType ContentType { get; }
        string Filename { get; }
        string Permalink { get; }
        bool Published { get; }
    }
}
