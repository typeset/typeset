using System;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace Typeset.Web.CoffeeScript.BundleTransforms
{
    public class CoffeeScriptCompile : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = string.Empty;
            response.ContentType = "text/javascript";
            response.Cacheability = HttpCacheability.Public;

            foreach (var file in response.Files)
            {
                var fileText = File.ReadAllText(file.FullName);
                if (file.Extension.Equals(".coffee", StringComparison.OrdinalIgnoreCase))
                {
                    response.Content += CoffeeScript.Compiler.Compile(fileText);
                }
                else
                {
                    response.Content += fileText;
                }
            }
        }
    }
}