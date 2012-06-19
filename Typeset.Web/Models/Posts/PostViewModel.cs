using Typeset.Domain.Post;
using Typeset.Web.Models.Common;
using Typeset.Domain.Markup;

namespace Typeset.Web.Models.Posts
{
    public class PostViewModel
    {
        public DateViewModel Date { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string HtmlContent { get; set; }

        public PostViewModel(IPost entity, IMarkupProcessorFactory markupProcessorFactory)
        {
            Date = new DateViewModel(entity.Date);
            Title = entity.Title;
            Content = entity.Content;
            ContentType = entity.ContentType.ToString();
            HtmlContent = GenerateHtmlContent(entity.ContentType, markupProcessorFactory);
        }

        protected virtual string GenerateHtmlContent(ContentType contentType, IMarkupProcessorFactory markupProcessorFactory)
        {
            switch (contentType)
            {
                case Domain.Post.ContentType.markdown:
                    return markupProcessorFactory.CreateInstance(ProcessorType.markdown).Process(Content);
                case Domain.Post.ContentType.textile:
                    return markupProcessorFactory.CreateInstance(ProcessorType.textile).Process(Content);
                default:
                    return string.Empty;
            }
        }
    }
}