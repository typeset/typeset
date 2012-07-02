﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Typeset.Domain.Common;

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
            entities = entities.Where(p => p.Permalink.ToLower().Contains(searchCriteria.Permalink.ToLower())).ToList();
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
                var fileText = File.ReadAllText(path);

                var filename = Path.GetFileName(path);
                var date = FrontMatter.Yaml.ParseDate(path);
                var title = FrontMatter.Yaml.ParseTitle(path, fileText);
                var content = FrontMatter.Yaml.ParseContent(fileText);
                var contentType = FrontMatter.Yaml.ParseContentType(path);

                var permalink = FrontMatter.Yaml.ParsePermalink(path, fileText, 11);
                var tags = FrontMatter.Yaml.ParseTags(fileText);
                var published = FrontMatter.Yaml.ParsePublished(fileText);

                post = new Post(date, title, content, contentType, filename, permalink, tags, published);
                success = true;
            }
            catch 
            {
                post = null;
            }

            return success;
        }
    }
}
