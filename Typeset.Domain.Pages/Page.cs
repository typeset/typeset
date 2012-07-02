using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Pages
{
    internal class Page : IPage
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public ContentType ContentType { get; private set; }
        public string Filename { get; private set; }
        public string Permalink { get; private set; }
        public bool Published { get; private set; }

        public Page(string title, string content, ContentType contentType, string filename, string permalink, bool published)
        {
            Title = title;
            Content = content;
            ContentType = contentType;
            Filename = filename;
            Permalink = permalink;
            Published = published;
        }
    }
}
