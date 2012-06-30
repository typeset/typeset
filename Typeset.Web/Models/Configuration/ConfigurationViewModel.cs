using Typeset.Domain.Configuration;

namespace Typeset.Web.Models.Configuration
{
    public class ConfigurationViewModel
    {
        public string DisqusShortname { get; private set; }

        public ConfigurationViewModel(IConfiguration entity)
        {
            DisqusShortname = entity.DisqusShortname;
        }
    }
}