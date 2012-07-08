using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Typeset.Domain.Common;
using YamlDotNet.RepresentationModel;

namespace Typeset.Domain.FrontMatter
{
    public static class FrontMatterParser
    {
        public static IEnumerable<string> HtmlExtensions = new string[] { ".html", ".htm" };
        public static IEnumerable<string> MarkdownExtensions = new string[] { ".md", ".mkd", ".mkdn", ".mdwn", ".mdown", ".markdown" };
        public static IEnumerable<string> TextileExtensions = new string[] { ".textile" };
        public static IEnumerable<string> FrontMatterExtensions = Enumerable.Concat(HtmlExtensions, MarkdownExtensions).Concat(TextileExtensions);

        public static bool IsFrontMatterExtension(string extension)
        {
            return FrontMatterExtensions.Any(ext => ext.ToLower().Equals(extension.ToLower()));
        }

        public static class Yaml
        {
            public static bool HasFrontMatter(string path)
            {
                var hasFrontMatter = false;

                try
                {
                    if (IsFrontMatterExtension(Path.GetExtension(path)))
                    {
                        var fileText = File.ReadAllText(path);
                        var yamlDocument = ParseYaml(fileText);
                        hasFrontMatter = true;
                    }
                }
                catch { }

                return hasFrontMatter;
            }

            public static bool TryParseFrontMatter(string path, out IFrontMatter frontMatter)
            {
                var success = false;

                try
                {
                    frontMatter = ParseFrontMatter(path);
                    success = true;
                }
                catch
                {
                    frontMatter = null;
                }

                return success;
            }

            public static IFrontMatter ParseFrontMatter(string path)
            {
                var fileText = File.ReadAllText(path);

                if (!HasFrontMatter(path))
                {
                    throw new ArgumentException("FrontMatter not found");
                }

                var frontMatter = new FrontMatter();
                frontMatter.Content = ParseContent(fileText);
                frontMatter.ContentType = ParseContentType(path);
                frontMatter.Layout = ParseLayout(fileText);
                frontMatter.DateTime = ParseDateTime(fileText);
                frontMatter.Filename = Path.GetFileName(path);
                frontMatter.Permalinks = ParsePermalink(fileText);
                frontMatter.Published = ParsePublished(fileText);
                frontMatter.Tags = ParseTags(fileText);
                frontMatter.Title = ParseTitle(fileText);

                return frontMatter;
            }

            internal static YamlDocument ParseYaml(string fileText)
            {
                YamlDocument yamlDocument;

                var yamlStream = new YamlStream();
                using (var stringReader = new StringReader(fileText))
                {
                    yamlStream.Load(stringReader);
                }

                yamlDocument = yamlStream.Documents[0];

                if (!(yamlDocument.RootNode is YamlScalarNode || yamlDocument.RootNode is YamlMappingNode))
                {
                    throw new ArgumentException("Yaml document not found");
                }

                return yamlDocument;
            }

            internal static string ParseLayout(string fileText)
            {
                var layout = string.Empty;

                try
                {
                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("layout")))
                    {
                        layout = yamlMapping.Children[new YamlScalarNode("layout")].ToString();
                    }
                }
                catch { }

                return layout;
            }

            internal static DateTimeOffset? ParseDateTime(string fileText)
            {
                DateTimeOffset? dateTime = null;

                try
                {
                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("date")))
                    {
                        var dateTimeString = yamlMapping.Children[new YamlScalarNode("date")].ToString();
                        dateTime = DateTimeOffset.Parse(dateTimeString).UtcDateTime;
                    }
                }
                catch { }

                return dateTime;
            }

            internal static string ParseContent(string fileText)
            {
                var content = string.Empty;

                try
                {
                    var regex = new Regex(@"^(---\s)([\s\S]*?)(\s---)(\s*)");
                    if (regex.IsMatch(fileText))
                    {
                        var frontMatter = regex.Match(fileText).Value;
                        content = fileText.Replace(frontMatter, string.Empty);
                    }
                }
                catch { }

                return content;
            }

            internal static string ParseTitle(string fileText)
            {
                var title = string.Empty;

                try
                {
                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("title")))
                    {
                        title = yamlMapping.Children[new YamlScalarNode("title")].ToString();
                    }
                }
                catch { }

                return title;
            }

            internal static IEnumerable<string> ParsePermalink(string fileText)
            {
                var permalinks = new List<string>();

                try
                {
                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("permalink")))
                    {
                        var yamlPermalinks = (YamlSequenceNode)yamlMapping.Children[new YamlScalarNode("permalink")];
                        foreach (var tag in yamlPermalinks.Children)
                        {
                            permalinks.Add(tag.ToString());
                        }
                    }
                }
                catch { }

                if (!permalinks.Any())
                {
                    throw new FormatException("permalink not found");
                }

                return permalinks;
            }

            internal static IEnumerable<string> ParseTags(string fileText)
            {
                var tags = new List<string>();

                try
                {
                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("tags")))
                    {
                        var yamlTags = (YamlSequenceNode)yamlMapping.Children[new YamlScalarNode("tags")];
                        foreach (var tag in yamlTags.Children)
                        {
                            tags.Add(tag.ToString());
                        }
                    }
                }
                catch { }

                return tags;
            }

            internal static bool ParsePublished(string fileText)
            {
                var published = true;

                try
                {
                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("published")))
                    {
                        published = yamlMapping.Children[new YamlScalarNode("published")].ToString().Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase);
                    }
                }
                catch { }

                return published;
            }

            internal static ContentType ParseContentType(string path)
            {
                var extension = Path.GetExtension(path).ToLower();

                if (MarkdownExtensions.Any(ext => ext.Equals(extension)))
                {
                    return ContentType.markdown;
                }
                else if (TextileExtensions.Any(ext => ext.Equals(extension)))
                {
                    return ContentType.textile;
                }
                else if (HtmlExtensions.Any(ext => ext.Equals(extension)))
                {
                    return ContentType.html;
                }
                else
                {
                    throw new Exception("Unknown content type");
                }
            }
        }
    }
}
