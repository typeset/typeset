using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Post
{
    internal class Post : IPost
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public ContentType ContentType { get; private set; }

        public Post(int year, int month, int day, string title, string content, ContentType contentType)
        {
            Year = year;
            Month = month;
            Day = day;
            Title = title;
            Content = content;
            ContentType = contentType;
        }
    }
}
