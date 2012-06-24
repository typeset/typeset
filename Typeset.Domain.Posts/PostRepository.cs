using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NodaTime;
using Typeset.Domain.Common;
using YamlDotNet.RepresentationModel;

namespace Typeset.Domain.Post
{
    public class PostRepository : IPostRepository
    {
        public virtual PageOf<IPost, PostSearchCriteria> Get(PostSearchCriteria searchCriteria)
        {
            var entities = new List<IPost>();

            var allFiles = GetAllFiles(searchCriteria.Path);

            foreach (var file in allFiles)
            {
                IPost entity;
                if (TryParseFile(file, out entity))
                {
                    entities.Add(entity);
                }
            }
            
            entities = entities.Where(p => p.Date >= searchCriteria.From && p.Date <= searchCriteria.To).ToList();
            entities = entities.Where(p => p.Filename.ToLower().Contains(searchCriteria.Filename.ToLower())).ToList();
            entities = entities.Where(p => p.Published.Equals(searchCriteria.Published)).ToList();
            var totalCount = entities.Count;
            entities = searchCriteria.Order == Order.Ascending ? entities.OrderBy(e => e.Date).ThenBy(e => e.Title).ToList() : entities.OrderByDescending(e => e.Date).ThenByDescending(e => e.Title).ToList();
            entities = entities.Count > searchCriteria.Offset ? entities.Skip(searchCriteria.Offset).Take(searchCriteria.Limit).ToList() : new List<IPost>();

            return new PageOf<IPost,PostSearchCriteria>(searchCriteria, entities, totalCount);
        }

        protected virtual IEnumerable<string> GetAllFiles(string path)
        {
            var entities = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
            return entities;
        }

        protected virtual bool TryParseFile(string path, out IPost post)
        {
            var success = false;

            try
            {
                var yamlDocument = ParseFrontMatter(path);

                var filename = Path.GetFileName(path);
                var date = ParseDate(path);
                var title = ParseTitle(path, yamlDocument);
                var content = File.ReadAllText(path);
                var contentType = ParseContentType(path);
                
                var permalink = ParsePermalink(yamlDocument);
                var tags = ParseTags(yamlDocument);
                var published = ParsePublished(yamlDocument);

                post = new Post(date, title, content, contentType, filename, permalink, tags, published);
                success = true;
            }
            catch 
            {
                post = null;
            }

            return success;
        }

        protected virtual LocalDate ParseDate(string path)
        {
            var filenameNoExtension = Path.GetFileNameWithoutExtension(path);
            var year = int.Parse(filenameNoExtension.Substring(0, 4));
            var month = int.Parse(filenameNoExtension.Substring(5, 2));
            var day = int.Parse(filenameNoExtension.Substring(8, 2));
            return new LocalDate(year, month, day);
        }

        protected virtual ContentType ParseContentType(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".md":
                case ".mkd":
                case ".mkdn":
                case ".mdwn":
                case ".mdown":
                case ".markdown":
                    return ContentType.markdown;
                case ".textile":
                    return ContentType.textile;
                default:
                    throw new Exception("Unknown content type");
            }
        }

        protected virtual YamlDocument ParseFrontMatter(string path)
        {
            var yamlStream = new YamlStream();
            var fileText = File.ReadAllText(path);

            using (var stringReader = new StringReader(fileText))
            {
                yamlStream.Load(stringReader);
            }

            return yamlStream.Documents[0];
        }

        protected virtual string ParseTitle(string path, YamlDocument yamlDocument)
        {
            var title = string.Empty;

            try
            {
                var filenameNoExtension = Path.GetFileNameWithoutExtension(path);
                title = filenameNoExtension.Substring(11);

                var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                if (yamlMapping.Children.ContainsKey(new YamlScalarNode("title")))
                {
                    title = yamlMapping.Children[new YamlScalarNode("title")].ToString();
                }
            }
            catch { }

            return title;
        }

        protected virtual string ParsePermalink(YamlDocument yamlDocument)
        {
            var permalink = string.Empty;
            
            try
            {
                var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                if (yamlMapping.Children.ContainsKey(new YamlScalarNode("permalink")))
                {
                    permalink = yamlMapping.Children[new YamlScalarNode("permalink")].ToString();
                }
            }
            catch { }

            return permalink;
        }

        protected IEnumerable<string> ParseTags(YamlDocument yamlDocument)
        {
            var tags = new List<string>();

            try
            {
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

        protected virtual bool ParsePublished(YamlDocument yamlDocument)
        {
            var published = true;

            try
            {
                var yamlMapping = (YamlMappingNode)yamlDocument.RootNode;
                if (yamlMapping.Children.ContainsKey(new YamlScalarNode("published")))
                {
                    published = yamlMapping.Children[new YamlScalarNode("published")].ToString().Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase);
                }
            }
            catch { }

            return published;
        }
    }
}
