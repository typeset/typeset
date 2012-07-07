using System.Collections.Generic;
using SassAndCoffee.Ruby.Sass;

namespace Typeset.Web.Sass
{
    public static class Compiler
    {
        private static SassCompiler SassCompiler = new SassCompiler();

        public static string Compile(string fileText)
        {
            try
            {
                return SassCompiler.Compile(fileText, false, new List<string>());
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
