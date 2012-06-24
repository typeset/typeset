using System;
using NodaTime;
using System.Collections.Generic;

namespace Typeset.Domain.Post
{
    internal class Post : IPost
    {
        public LocalDate Date { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public ContentType ContentType { get; private set; }
        public string Filename { get; private set; }
        public string Permalink { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public bool Published { get; private set; }

        public Post(LocalDate date, string title, string content, ContentType contentType, string filename, string permalink, IEnumerable<string> tags, bool published)
        {
            if (date == null)
            {
                throw new ArgumentNullException("date");
            }

            Date = date;
            Title = title;
            Content = content;
            ContentType = contentType;
            Filename = filename;
            Permalink = permalink;
            Tags = tags;
            Published = published;
        }
    }
}
