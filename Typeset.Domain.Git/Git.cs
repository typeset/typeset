using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Git
{
    public static class Git
    {
        public static void Clone(string url, string path)
        {
            GitSharp.Git.Clone(url, path);
        }
    }
}
