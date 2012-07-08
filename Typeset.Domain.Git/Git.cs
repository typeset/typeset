using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Git
{
    public static class Git
    {
        public static void Pull(string path)
        {
            var repository = NGit.Api.Git.Open(path);
            var command = repository.Pull();
            var result = command.Call();
        }

        public static void Clone(string url, string path)
        {
            var command = new NGit.Api.CloneCommand();
            command.SetURI(url);
            command.SetDirectory(new Sharpen.FilePath(path));
            var result = command.Call();
        }
    }
}
