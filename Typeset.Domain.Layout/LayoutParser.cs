using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;
using System.IO;

namespace Typeset.Domain.Layout
{
    public static class LayoutParser
    {
        public static ILayout Parse(string path)
        {
            var entity = new Layout();

            try
            {
                var yamlStream = new YamlStream();
                var fileText = File.ReadAllText(path);
                using (var stringReader = new StringReader(fileText))
                {
                    yamlStream.Load(stringReader);
                }
                var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;

                if (rootNode.Children.ContainsKey(new YamlScalarNode("head")))
                {
                    var headNode = (YamlMappingNode)rootNode.Children[new YamlScalarNode("head")];

                    if (headNode.Children.ContainsKey(new YamlScalarNode("html")))
                    {
                        if (headNode.Children[new YamlScalarNode("html")] is YamlSequenceNode)
                        {
                            var htmlNode = (YamlSequenceNode)headNode.Children[new YamlScalarNode("html")];
                            var urls = new List<string>();
                            foreach (var url in htmlNode.Children)
                            {
                                urls.Add(url.ToString());
                            }
                            entity.HeadHtml = urls;
                        }
                    }

                    if (headNode.Children.ContainsKey(new YamlScalarNode("styles")))
                    {
                        if (headNode.Children[new YamlScalarNode("styles")] is YamlSequenceNode)
                        {
                            var stylesNode = (YamlSequenceNode)headNode.Children[new YamlScalarNode("styles")];
                            var urls = new List<string>();
                            foreach (var url in stylesNode.Children)
                            {
                                urls.Add(url.ToString());
                            }
                            entity.HeadStyles = urls;
                        }
                    }

                    if (headNode.Children.ContainsKey(new YamlScalarNode("scripts")))
                    {
                        if (headNode.Children[new YamlScalarNode("scripts")] is YamlSequenceNode)
                        {
                            var scriptsNode = (YamlSequenceNode)headNode.Children[new YamlScalarNode("scripts")];
                            var urls = new List<string>();
                            foreach (var url in scriptsNode.Children)
                            {
                                urls.Add(url.ToString());
                            }
                            entity.HeadScripts = urls;
                        }
                    }
                }

                if (rootNode.Children.ContainsKey(new YamlScalarNode("body")))
                {
                    var bodyNode = (YamlMappingNode)rootNode.Children[new YamlScalarNode("body")];

                    if (bodyNode.Children.ContainsKey(new YamlScalarNode("pre_content")))
                    {
                        if (bodyNode.Children[new YamlScalarNode("pre_content")] is YamlSequenceNode)
                        {
                            var preContentNode = (YamlSequenceNode)bodyNode.Children[new YamlScalarNode("pre_content")];
                            var urls = new List<string>();
                            foreach (var url in preContentNode.Children)
                            {
                                urls.Add(url.ToString());
                            }
                            entity.BodyHtmlPreContent = urls;
                        }
                    }

                    if (bodyNode.Children.ContainsKey(new YamlScalarNode("post_content")))
                    {
                        if (bodyNode.Children[new YamlScalarNode("post_content")] is YamlSequenceNode)
                        {
                            var postContentNode = (YamlSequenceNode)bodyNode.Children[new YamlScalarNode("post_content")];
                            var urls = new List<string>();
                            foreach (var url in postContentNode.Children)
                            {
                                urls.Add(url.ToString());
                            }
                            entity.BodyHtmlPostContent = urls;
                        }
                    }

                    if (bodyNode.Children.ContainsKey(new YamlScalarNode("scripts")))
                    {
                        if (bodyNode.Children[new YamlScalarNode("scripts")] is YamlSequenceNode)
                        {
                            var scriptsNode = (YamlSequenceNode)bodyNode.Children[new YamlScalarNode("scripts")];
                            var urls = new List<string>();
                            foreach (var url in scriptsNode.Children)
                            {
                                urls.Add(url.ToString());
                            }
                            entity.BodyScripts = urls;
                        }
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
