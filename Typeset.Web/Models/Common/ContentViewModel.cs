using System;
using System.Collections.Generic;
using Typeset.Domain.FrontMatter;
using Typeset.Domain.Markup;

namespace Typeset.Web.Models.Common
{
    public class ContentViewModel
    {
        public DateTimeOffset Date { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string HtmlContent { get; set; }
        public IEnumerable<string> Permalinks { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public ContentViewModel(IFrontMatter entity, IMarkupProcessorFactory markupProcessorFactory)
        {
            Date = entity.DateTime.HasValue ? entity.DateTime.Value : DateTimeOffset.MinValue;
            Title = entity.Title;
            Content = entity.Content;
            ContentType = entity.ContentType.ToString();
            HtmlContent = markupProcessorFactory.CreateInstance(entity.ContentType).Process(Content);
            Permalinks = entity.Permalinks;
            Tags = entity.Tags;
        }
    }
}