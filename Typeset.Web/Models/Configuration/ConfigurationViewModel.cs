using Typeset.Domain.Configuration;

namespace Typeset.Web.Models.Configuration
{
    public class ConfigurationViewModel
    {
        public string DateFormat { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }

        public ConfigurationViewModel(IConfiguration entity)
        {
            DateFormat = string.IsNullOrWhiteSpace(entity.DateFormat) ? string.Empty : entity.DateFormat;
            Author = string.IsNullOrWhiteSpace(entity.Author) ? string.Empty : entity.Author;
            Title = string.IsNullOrWhiteSpace(entity.Title) ? string.Empty : entity.Title;
        }
    }
}