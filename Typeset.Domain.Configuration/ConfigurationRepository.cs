using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Typeset.Domain.Configuration
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        public IConfiguration Read(string path)
        {
            var entity = new Configuration();

            try
            {
                var yamlStream = new YamlStream();
                var fileText = File.ReadAllText(path);
                using (var stringReader = new StringReader(fileText))
                {
                    yamlStream.Load(stringReader);
                }
                var mapping = (YamlMappingNode)yamlStream.Documents[0].RootNode;
            }
            catch
            {
            }

            return entity;
        }
    }
}
