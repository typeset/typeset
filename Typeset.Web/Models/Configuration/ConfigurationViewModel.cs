using Typeset.Domain.Configuration;

namespace Typeset.Web.Models.Configuration
{
    public class ConfigurationViewModel
    {
        public bool DisqusDeveloperMode { get; private set; }
        public string DisqusShortname { get; private set; }

        public ConfigurationViewModel(IConfiguration entity)
        {
            DisqusDeveloperMode = entity.DisqusDeveloperMode;
            DisqusShortname = entity.DisqusShortname;
        }
    }
}