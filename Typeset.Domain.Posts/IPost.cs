using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Post
{
    public enum ContentType
    {
        markdown,
        textile
    }

    public interface IPost
    {
        int Year { get; }
        int Month { get; }
        int Day { get; }
        string Title { get; }
        string Content { get; }
        ContentType ContentType { get; }
    }
}
