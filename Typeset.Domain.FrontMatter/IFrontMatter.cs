using System.Collections.Generic;
using NodaTime;
using Typeset.Domain.Common;

namespace Typeset.Domain.FrontMatter
{
    public interface IFrontMatter
    {
        string Content { get; }
        ContentType ContentType { get; }
        string Layout { get; }
        LocalDate? Date { get; }
        LocalTime? Time { get; }
        string Filename { get; }
        string Permalink { get; }
        bool Published { get; }
        IEnumerable<string> Tags { get; }
        string Title { get; }
    }
}
