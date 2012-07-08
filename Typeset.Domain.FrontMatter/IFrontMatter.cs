using System;
using System.Collections.Generic;
using Typeset.Domain.Common;

namespace Typeset.Domain.FrontMatter
{
    public interface IFrontMatter
    {
        string Content { get; }
        ContentType ContentType { get; }
        string Layout { get; }
        DateTimeOffset? DateTime { get; }
        string Filename { get; }
        IEnumerable<string> Permalinks { get; }
        bool Published { get; }
        IEnumerable<string> Tags { get; }
        string Title { get; }
    }
}
