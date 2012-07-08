using System;
using System.Collections.Generic;
using Typeset.Domain.Common;

namespace Typeset.Domain.FrontMatter
{
    internal class FrontMatter : IFrontMatter
    {
        public string Content { get; set; }
        public ContentType ContentType { get; set; }
        public string Layout { get; set; }
        public DateTimeOffset? DateTime { get; set; }
        public string Filename { get; set; }
        public IEnumerable<string> Permalinks { get; set; }
        public bool Published { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Title { get; set; }

        public FrontMatter()
        {
            Permalinks = new List<string>();
            Tags = new List<string>();
            Published = true;
        }
    }
}
