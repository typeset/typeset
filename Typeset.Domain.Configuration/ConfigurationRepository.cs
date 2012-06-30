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

                if (mapping.Children.ContainsKey(new YamlScalarNode("disqus")))
                {
                    var nameNode = (YamlMappingNode)mapping.Children[new YamlScalarNode("disqus")];
                    if (nameNode.Children.ContainsKey(new YamlScalarNode("shortname")))
                    {
                        entity.DisqusShortname = nameNode.Children[new YamlScalarNode("shortname")].ToString();
                    }
                }
            }
            catch
            {
            }

            return entity;
        }
    }
}
