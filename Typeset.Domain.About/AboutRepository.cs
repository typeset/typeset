﻿using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Typeset.Domain.About
{
    public class AboutRepository : IAboutRepository
    {
        public IAbout Read(string path)
        {
            var entity = new About();

            try
            {
                var yamlStream = new YamlStream();
                var fileText = File.ReadAllText(path);
                using (var stringReader = new StringReader(fileText))
                {
                    yamlStream.Load(stringReader);
                }
                var mapping = (YamlMappingNode)yamlStream.Documents[0].RootNode;

                if (mapping.Children.ContainsKey(new YamlScalarNode("name")))
                {
                    var nameNode = (YamlMappingNode)mapping.Children[new YamlScalarNode("name")];
                    if (nameNode.Children.ContainsKey(new YamlScalarNode("first")))
                    {
                        entity.FirstName = nameNode.Children[new YamlScalarNode("first")].ToString();
                    }

                    if (nameNode.Children.ContainsKey(new YamlScalarNode("last")))
                    {
                        entity.LastName = nameNode.Children[new YamlScalarNode("last")].ToString();
                    }
                }

                if (mapping.Children.ContainsKey(new YamlScalarNode("bio")))
                {
                    entity.Bio = mapping.Children[new YamlScalarNode("bio")].ToString();
                }

                if (mapping.Children.ContainsKey(new YamlScalarNode("email")))
                {
                    entity.Email = mapping.Children[new YamlScalarNode("email")].ToString();
                }

                if (mapping.Children.ContainsKey(new YamlScalarNode("usernames")))
                {
                    var nameNode = (YamlMappingNode)mapping.Children[new YamlScalarNode("usernames")];
                    foreach (var key in nameNode.Children.Keys)
                    {
                        entity.Usernames.Add(key.ToString(), nameNode.Children[key].ToString());
                    }
                }

                if (mapping.Children.ContainsKey(new YamlScalarNode("links")))
                {
                    foreach (YamlMappingNode node in (YamlSequenceNode)mapping.Children[new YamlScalarNode("links")])
                    {
                        var description = node.Children.First().Key.ToString();
                        var url = node.First().Value.ToString();
                        entity.Links.Add(description, url);
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
