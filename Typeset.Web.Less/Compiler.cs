using System;

namespace Typeset.Web.Less
{
    public static class Compiler
    {
        public static string Compile(string fileText)
        {
            return dotless.Core.Less.Parse(fileText);
        }
    }
}
