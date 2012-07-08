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
                
                if (mapping.Children.ContainsKey(new YamlScalarNode("dateformat")))
                {
                    entity.DateFormat = mapping.Children[new YamlScalarNode("dateformat")].ToString();
                }

                if (mapping.Children.ContainsKey(new YamlScalarNode("title")))
                {
                    entity.Title = mapping.Children[new YamlScalarNode("title")].ToString();
                }

                if (mapping.Children.ContainsKey(new YamlScalarNode("author")))
                {
                    entity.Author = mapping.Children[new YamlScalarNode("author")].ToString();
                }
            }
            catch
            {
            }

            return entity;
        }
    }
}
