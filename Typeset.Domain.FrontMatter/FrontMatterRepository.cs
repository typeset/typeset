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
            var entities = new List<IFrontMatter>();
            var allFiles = GetAllFiles(searchCriteria.Path);

            foreach (var file in allFiles)
            {
                IFrontMatter entity;
                if (FrontMatterParser.Yaml.TryParseFrontMatter(file, out entity))
                {
                    entities.Add(entity);
                }
            }

            entities = entities.Where(p => p.Published.Equals(searchCriteria.Published)).ToList();

            if (searchCriteria.From.HasValue)
            {
                entities = entities.Where(p => p.Date >= searchCriteria.From).ToList();
            }
            if (searchCriteria.To.HasValue)
            {
                entities = entities.Where(p => p.Date <= searchCriteria.To).ToList();
            }
            if (!string.IsNullOrWhiteSpace(searchCriteria.Permalink))
            {
                entities = entities.Where(p => p.Permalink.ToLower().Contains(searchCriteria.Permalink.ToLower())).ToList();
            }
            
            var totalCount = entities.Count;

            if (searchCriteria.Order == Order.Ascending)
            {
                entities = entities.OrderBy(e => e.Date).ThenBy(e => e.Title).ToList();
            }
            else
            {
                entities = entities.OrderByDescending(e => e.Date).ThenByDescending(e => e.Title).ToList();
            }

            entities = entities.Count > searchCriteria.Offset ? entities.Skip(searchCriteria.Offset).Take(searchCriteria.Limit).ToList() : new List<IFrontMatter>();

            return new PageOf<IFrontMatter, FrontMatterSearchCriteria>(searchCriteria, entities, totalCount);
        }

        protected virtual IEnumerable<string> GetAllFiles(string path)
        {
            var entities = new List<string>();

            foreach (var extension in FrontMatterParser.FrontMatterExtensions)
            {
                var searchPattern = string.Format("*{0}", extension);
                var files = Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);
                files = files.Where(e => !e.Substring(path.Length).Contains("\\_"));
                entities.AddRange(files);
            }

            return entities;
        }
    }
}
