using System.Configuration;

namespace Typeset.Web.Configuration
{
    public interface IConfigurationManager
    {
        ReadOnlyDictionary<string, string> AppSettings { get; }
        ReadOnlyDictionary<string, ConnectionStringSettings> ConnectionStrings { get; }
    }
}
