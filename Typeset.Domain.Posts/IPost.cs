using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Post
{
    public enum ContentType
    {
        markdown,
        textile
    }

    public interface IPost
    {
        Date Date { get; }
        string Title { get; }
        string Content { get; }
        ContentType ContentType { get; }
    }
}
