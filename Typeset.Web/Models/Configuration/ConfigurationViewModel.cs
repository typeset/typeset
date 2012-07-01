using Typeset.Domain.Configuration;

namespace Typeset.Web.Models.Configuration
{
    public class ConfigurationViewModel
    {
        public string DateFormat { get; set; }
        public string SyndicationAuthor { get; set; }
        public string SyndicationTitle { get; set; }

        public ConfigurationViewModel(IConfiguration entity)
        {
            DateFormat = entity.DateFormat;
            SyndicationAuthor = entity.SyndicationAuthor;
            SyndicationTitle = entity.SyndicationTitle;
        }
    }
}