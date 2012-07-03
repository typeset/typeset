using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NodaTime;
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

        public static class Yaml
        {
            public static bool HasFrontMatter(string path)
            {
                var hasFrontMatter = false;

                try
                {
                    var extension = Path.GetExtension(path).ToLower();
                    if (FrontMatterExtensions.Any(ext => ext.Equals(extension)))
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
                frontMatter.Date = ParseDate(path, fileText);
                frontMatter.Filename = Path.GetFileNameWithoutExtension(path);
                frontMatter.Permalink = ParsePermalink(path, fileText);
                frontMatter.Published = ParsePublished(fileText);
                frontMatter.Tags = ParseTags(fileText);
                frontMatter.Title = ParseTitle(path, fileText);

                return frontMatter;
            }

            private static YamlDocument ParseYaml(string fileText)
            {
                YamlDocument yamlDocument;

                var yamlStream = new YamlStream();
                using (var stringReader = new StringReader(fileText))
                {
                    yamlStream.Load(stringReader);
                }

                yamlDocument = yamlStream.Documents[0];
                var rootNode = (YamlMappingNode)yamlDocument.RootNode; //throws exception when not valid yaml, we want this.

                return yamlDocument;
            }

            private static LocalDate? ParseDate(string path, string fileText)
            {
                LocalDate? date = null;

                if (path.ToLower().Contains("_posts"))
                {
                    var filenameNoExtension = Path.GetFileNameWithoutExtension(path);
                    var year = int.Parse(filenameNoExtension.Substring(0, 4));
                    var month = int.Parse(filenameNoExtension.Substring(5, 2));
                    var day = int.Parse(filenameNoExtension.Substring(8, 2));
                    date = new LocalDate(year, month, day);
                }

                try
                {
                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("date")))
                    {
                        var dateTimeString = yamlMapping.Children[new YamlScalarNode("date")].ToString();
                        var dateTime = DateTimeOffset.Parse(dateTimeString);
                        date = new LocalDate(dateTime.Year, dateTime.Month, dateTime.Day);
                    }
                }
                catch { }

                return date;
            }

            private static LocalTime? ParseTime(string path, string fileText)
            {
                LocalTime? time = null;

                try
                {
                    var yamlDocument = ParseYaml(fileText);
                    var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                    if (yamlMapping.Children.ContainsKey(new YamlScalarNode("date")))
                    {
                        var dateTimeString = yamlMapping.Children[new YamlScalarNode("date")].ToString();
                        var dateTime = DateTimeOffset.Parse(dateTimeString);
                        time = new LocalTime(dateTime.Hour, dateTime.Minute, dateTime.Second);
                    }
                }
                catch { }

                return time;
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
                    if (path.ToLower().Contains("_posts"))
                    {
                        var filenameNoExtension = Path.GetFileNameWithoutExtension(path);
                        title = filenameNoExtension.Substring(11);
                    }

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

            private static string ParsePermalink(string path, string fileText)
            {
                var permalink = string.Empty;

                try
                {
                    var startOffset = path.ToLower().Contains("_posts") ? 11 : 0;
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
