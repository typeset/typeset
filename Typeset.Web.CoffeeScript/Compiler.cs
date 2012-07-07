using CoffeeSharp;

namespace Typeset.Web.CoffeeScript
{
    public static class Compiler
    {
        private static CoffeeScriptEngine CoffeeScriptEngine = new CoffeeScriptEngine();

        public static string Compile(string code)
        {
            return CoffeeScriptEngine.Compile(code);
        }
    }
}
