using System;
using NodaTime;

namespace Typeset.Domain.Post
{
    internal class Post : IPost
    {
        public LocalDate Date { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public ContentType ContentType { get; private set; }
        public string Filename { get; private set; }

        public Post(LocalDate date, string title, string content, ContentType contentType, string filename)
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
        }
    }
}
