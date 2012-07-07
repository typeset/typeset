using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Typeset.Web.CoffeeScript.BundleTransforms
{
    public class CoffeeScriptCompile : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var coffeeScriptFiles = response.Files.Where(f => f.Extension.Equals(".coffee", StringComparison.OrdinalIgnoreCase));
            foreach (var file in coffeeScriptFiles)
            {
                using (var reader = new StreamReader(file.FullName))
                {
                    var coffeeScript = reader.ReadToEnd();
                    var compiledCoffeeScript = CoffeeScript.Compiler.Compile(coffeeScript);
                    reader.Close();
                    response.Content = response.Content.Replace(coffeeScript, compiledCoffeeScript);
                }
            }
            response.ContentType = "text/javascript";
            response.Cacheability = HttpCacheability.Public;
        }
    }
}