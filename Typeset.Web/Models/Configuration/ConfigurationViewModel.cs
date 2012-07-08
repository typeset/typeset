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
            DateFormat = entity.DateFormat;
            Author = entity.Author;
            Title = entity.Title;
        }
    }
}