using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Typeset.Domain.Common;
using NodaTime;

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
            entities = searchCriteria.Order == Order.Ascending ? entities.OrderBy(e => e.Date).ThenBy(e => e.Title).ToList() : entities.OrderByDescending(e => e.Date).ThenByDescending(e => e.Title).ToList();
            var totalCount = entities.Count;
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
                var filename = Path.GetFileNameWithoutExtension(path);
                var year = int.Parse(filename.Substring(0, 4));
                var month = int.Parse(filename.Substring(5, 2));
                var day = int.Parse(filename.Substring(8, 2));
                var date = new LocalDate(year, month, day);
                var title = filename.Substring(11);
                var content = File.ReadAllText(path);
                var contentType = ParseContentType(path);

                post = new Post(date, title, content, contentType);
                success = true;
            }
            catch 
            {
                post = null;
            }

            return success;
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
    }
}
