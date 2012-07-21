using System;
using System.IO;
using System.Linq;

namespace Typeset.Domain.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private bool IsGettingLatest { get; set; }

        public void CheckoutOrUpdate(string repositoryUri, string path)
        {
            if (!IsGettingLatest)
            {
                try
                {
                    IsGettingLatest = true;

                    if (string.IsNullOrWhiteSpace(repositoryUri))
                    {
                        throw new ArgumentNullException("repositoryUri");
                    }

                    if (string.IsNullOrWhiteSpace(path))
                    {
                        throw new ArgumentNullException("path");
                    }

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (repositoryUri.EndsWith(".git", System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (!Directory.EnumerateFileSystemEntries(path).Any())
                        {
                            Git.Git.Clone(repositoryUri, path);
                        }
                        else
                        {
                            Git.Git.Pull(path);
                        }
                    }
                }
                catch 
                {
                    throw;
                }
                finally
                {
                    IsGettingLatest = false;
                }
            }
        }
    }
}
