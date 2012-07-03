using System.Collections.Generic;
using System.IO;
using System.Linq;
using Typeset.Domain.Common;

namespace Typeset.Domain.Pages
{
    public class PageRepository : IPageRepository
    {
        public virtual PageOf<IPage, PageSearchCriteria> Get(PageSearchCriteria searchCriteria)
        {
            var entities = new List<IPage>();

            var allFiles = GetAllFiles(searchCriteria.Path);

            foreach (var file in allFiles)
            {
                IPage entity;
                if (TryParseFile(file, out entity))
                {
                    entities.Add(entity);
                }
            }

            entities = entities.Where(p => p.Permalink.ToLower().Contains(searchCriteria.Permalink.ToLower())).ToList();
            entities = entities.Where(p => p.Published.Equals(searchCriteria.Published)).ToList();
            var totalCount = entities.Count;
            entities = searchCriteria.Order == Order.Ascending ? entities.OrderBy(e => e.Title).ToList() : entities.OrderByDescending(e => e.Title).ToList();
            entities = entities.Count > searchCriteria.Offset ? entities.Skip(searchCriteria.Offset).Take(searchCriteria.Limit).ToList() : new List<IPage>();

            return new PageOf<IPage, PageSearchCriteria>(searchCriteria, entities, totalCount);
        }

        protected virtual IEnumerable<string> GetAllFiles(string path)
        {
            var entities = new List<string>();

            foreach (var extension in FrontMatter.FrontMatterExtensions)
            {
                var searchPattern = string.Format("*{0}", extension);
                var files = Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);
                files = files.Where(e => !e.Contains("\\_"));
                entities.AddRange(files);
            }

            return entities;
        }

        protected virtual bool TryParseFile(string path, out IPage post)
        {
            var success = false;

            try
            {
                var fileText = File.ReadAllText(path);

                var filename = Path.GetFileName(path);
                var title = FrontMatter.Yaml.ParseTitle(path, fileText);
                var content = FrontMatter.Yaml.ParseContent(fileText);
                var contentType = FrontMatter.Yaml.ParseContentType(path);

                var permalink = FrontMatter.Yaml.ParsePermalink(path, fileText, 0);
                var published = FrontMatter.Yaml.ParsePublished(fileText);

                post = new Page(title, content, contentType, filename, permalink, published);
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
