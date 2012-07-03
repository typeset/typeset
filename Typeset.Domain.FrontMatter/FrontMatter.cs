using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;
using NodaTime;

namespace Typeset.Domain.FrontMatter
{
    internal class FrontMatter : IFrontMatter
    {
        public string Content { get; set; }
        public ContentType ContentType { get; set; }
        public LocalDate? Date { get; set; }
        public LocalTime? Time { get; set; }
        public string Filename { get; set; }
        public string Permalink { get; set; }
        public bool Published { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Title { get; set; }

        public FrontMatter()
        {
            Tags = new List<string>();
            Published = true;
        }
    }
}
