using System.Collections.Generic;
using Typeset.Domain.Common;
using Typeset.Domain.Markup;
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
        public string HtmlContent { get; set; }
        public string Permalink { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public PostViewModel(IPost entity, IMarkupProcessorFactory markupProcessorFactory)
        {
            Date = new DateViewModel(entity.Date);
            Title = entity.Title;
            Content = entity.Content;
            ContentType = entity.ContentType.ToString();
            HtmlContent = markupProcessorFactory.CreateInstance(entity.ContentType).Process(Content);
            Permalink = entity.Permalink;
            Tags = entity.Tags;
        }
    }
}