using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Typeset.Domain.Post
{
    public class PostRepository : IPostRepository
    {
        private string Path { get; set; }

        public PostRepository(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            //if (!Directory.Exists(path))
            //{
            //    throw new DirectoryNotFoundException();
            //}

            Path = path;
        }

        public virtual IEnumerable<IPost> Get(SearchCriteria searchCriteria)
        {
            var entities = new List<IPost>();

            var allFiles = GetAllFiles();

            return entities;
        }

        protected virtual IEnumerable<string> GetAllFiles()
        {
            var entities = Directory.EnumerateFiles(Path, "*", SearchOption.AllDirectories);
            return entities;
        }
    }
}
