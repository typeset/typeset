using System.Collections.Generic;
using System.Configuration;

namespace Typeset.Web.Configuration
{
    public class ConfigurationManagerProxy : IConfigurationManager
    {
        public virtual ReadOnlyDictionary<string, string> AppSettings
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                foreach (var key in ConfigurationManager.AppSettings.AllKeys)
                {
                    dictionary.Add(key, ConfigurationManager.AppSettings[key]);
                }
                return new ReadOnlyDictionary<string,string>(dictionary);
            }
        }

        public virtual ReadOnlyDictionary<string, ConnectionStringSettings> ConnectionStrings
        {
            get
            {
                var dictionary = new Dictionary<string, ConnectionStringSettings>();
                for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
                {
                    var connectionStringSetting = ConfigurationManager.ConnectionStrings[i];
                    dictionary.Add(connectionStringSetting.Name, connectionStringSetting);
                }
                return new ReadOnlyDictionary<string, ConnectionStringSettings>(dictionary);
            }
        }
    }
}
