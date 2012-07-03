using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;
using Typeset.Domain.Common;
using NodaTime;
using System.IO;
using System.Text.RegularExpressions;

namespace Typeset.Domain.FrontMatter
{
    public static class FrontMatterParser
    {
        public static IEnumerable<string> HtmlExtensions = new string[] { ".html", ".htm" };
        public static IEnumerable<string> MarkdownExtensions = new string[] { ".md", ".mkd", ".mkdn", ".mdwn", ".mdown", ".markdown" };
        public static IEnumerable<string> TextileExtensions = new string[] { ".textile" };
        public static IEnumerable<string> FrontMatterExtensions
        {
            get
            {
                var extensions = new List<string>();
                extensions.AddRange(HtmlExtensions);
                extensions.AddRange(MarkdownExtensions);
                extensions.AddRange(TextileExtensions);
                return extensions;
            }
        }

        public static class Yaml
        {
            public static IFrontMatter ParseFrontMatter(string path)
            {
                var frontMatter = new FrontMatter();

                var fileText = File.ReadAllText(path);

                frontMatter.Content = ParseContent(fileText);
                frontMatter.ContentType = ParseContentType(path);
                frontMatter.Date = ParseDate(path, fileText);
                frontMatter.Filename = Path.GetFileNameWithoutExtension(path);
                frontMatter.Permalink = ParsePermalink(path, fileText, 0);
                frontMatter.Published = ParsePublished(fileText);
                frontMatter.Tags = ParseTags(fileText);
                frontMatter.Title = ParseTitle(path, fileText);

                return frontMatter;
            }

            public static YamlDocument ParseYaml(string fileText)
            {
                var yamlStream = new YamlStream();

                using (var stringReader = new StringReader(fileText))
                {
                    yamlStream.Load(stringReader);
                }

                return yamlStream.Documents[0];
            }

            private static LocalDate ParseDate(string path, string fileText)
            {
                var filenameNoExtension = Path.GetFileNameWithoutExtension(path);
                var year = int.Parse(filenameNoExtension.Substring(0, 4));
                var month = int.Parse(filenameNoExtension.Substring(5, 2));
                var day = int.Parse(filenameNoExtension.Substring(8, 2));
                return new LocalDate(year, month, day);
            }

            private static LocalTime ParseTime(string path, string fileText)
            {
                return new LocalTime();
            }

            private static string ParseContent(string fileText)
            {
                var content = fileText;
                var regex = new Regex(@"^(---\s)([\s\S]+?)(\s---)(\s*)");
                var hasFrontMatter = regex.IsMatch(fileText);

                if (hasFrontMatter)
                {
                    var frontMatter = regex.Match(fileText).Value;
                    var yamlStream = new YamlStream();

                    using (var stringReader = new StringReader(frontMatter))
                    {
                        yamlStream.Load(stringReader);
                    }

                    content = fileText.Replace(frontMatter, string.Empty);
                }

                return content;
            }

            private static string ParseTitle(string path, string fileText)
            {
                var title = string.Empty;

                try
                {
                    var filenameNoExtension = Path.GetFileNameWithoutExtension(path);
                    title = filenameNoExtension.Substring(11);

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

            private static string ParsePermalink(string path, string fileText, int startOffset)
            {
                var permalink = string.Empty;

                try
                {
                    var filenameNoExtension = Path.GetFileNameWithoutExtension(path);
                    permalink = filenameNoExtension.Substring(startOffset);

                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("permalink")))
                    {
                        permalink = yamlMapping.Children[new YamlScalarNode("permalink")].ToString();
                    }
                }
                catch { }

                return permalink;
            }

            private static IEnumerable<string> ParseTags(string fileText)
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

            private static bool ParsePublished(string fileText)
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

            private static ContentType ParseContentType(string path)
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
                else
                {
                    throw new Exception("Unknown content type");
                }
            }
        }
    }
}
