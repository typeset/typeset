using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Typeset.Domain.Post;
using Typeset.Web.Models.Common;

namespace Typeset.Web.Models.Posts
{
    public class PostViewModel
    {
        public DateViewModel Date { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }

        public PostViewModel()
        {
            Date = new DateViewModel();
        }

        public PostViewModel(IPost entity)
        {
            Date = new DateViewModel(entity.Date);
            Title = entity.Title;
            Content = entity.Content;
            ContentType = entity.ContentType.ToString();
        }
    }
}