using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Post
{
    internal class Post : IPost
    {
        public Date Date { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public ContentType ContentType { get; private set; }

        public Post(Date date, string title, string content, ContentType contentType)
        {
            if (date == null)
            {
                throw new ArgumentNullException("date");
            }

            Date = date;
            Title = title;
            Content = content;
            ContentType = contentType;
        }
    }
}
