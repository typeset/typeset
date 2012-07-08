using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Typeset.Domain.Common;

namespace Typeset.Domain.FrontMatter
{
    public class FrontMatterRepository : IFrontMatterRepository
    {
        public virtual PageOf<IFrontMatter, FrontMatterSearchCriteria> Get(FrontMatterSearchCriteria searchCriteria)
        {
            var entities = new List<IFrontMatter>() as IEnumerable<IFrontMatter>;

            entities = GetAllFrontMatterFiles(searchCriteria.Path);

            //Filter
            entities = entities.Where(p => p.Published.Equals(searchCriteria.Published));

            if (searchCriteria.From.HasValue)
            {
                entities = entities.Where(p => p.DateTime.HasValue && p.DateTime >= searchCriteria.From);
            }

            if (searchCriteria.To.HasValue)
            {
                entities = entities.Where(p => p.DateTime.HasValue && p.DateTime <= searchCriteria.To);
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.Permalink))
            {
                entities = entities.Where(e => e.Permalinks.Any(p => p.Equals(searchCriteria.Permalink, StringComparison.OrdinalIgnoreCase)));
            }
            
            var totalCount = entities.Count();

            //Cut
            if (searchCriteria.Order == Order.Ascending)
            {
                entities = entities.OrderBy(e => e.DateTime).ThenBy(e => e.Title);
            }
            else
            {
                entities = entities.OrderByDescending(e => e.DateTime).ThenByDescending(e => e.Title);
            }

            entities = entities.Count() > searchCriteria.Offset ? entities.Skip(searchCriteria.Offset).Take(searchCriteria.Limit) : new List<IFrontMatter>();

            return new PageOf<IFrontMatter, FrontMatterSearchCriteria>(searchCriteria, entities, totalCount);
        }

        protected virtual IEnumerable<IFrontMatter> GetAllFrontMatterFiles(string path)
        {
            var entities = new List<IFrontMatter>();
            var allFiles = GetAllFiles(path);

            foreach (var file in allFiles)
            {
                IFrontMatter entity;
                if (FrontMatterParser.Yaml.TryParseFrontMatter(file, out entity))
                {
                    entities.Add(entity);
                }
            }

            return entities;
        }

        protected virtual IEnumerable<string> GetAllFiles(string path)
        {
            var entities = new List<string>();

            foreach (var extension in FrontMatterParser.FrontMatterExtensions)
            {
                var searchPattern = string.Format("*{0}", extension);
                var files = Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);
                // ignore directories and files that start with an underscore
                files = files.Where(e => !e.Substring(path.Length).Contains("\\_"));
                entities.AddRange(files);
            }

            return entities;
        }
    }
}
