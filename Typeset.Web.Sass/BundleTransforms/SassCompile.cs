using System;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace Typeset.Web.Sass.BundleTransforms
{
    public class SassCompile : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = string.Empty;
            response.ContentType = "text/css";
            response.Cacheability = HttpCacheability.Public;

            foreach (var file in response.Files)
            {
                if (file.Extension.Equals(".sass", StringComparison.OrdinalIgnoreCase) ||
                    file.Extension.Equals(".scss", StringComparison.OrdinalIgnoreCase))
                {
                    response.Content += Sass.Compiler.Compile(file.FullName);
                }
                else
                {
                    response.Content += File.ReadAllText(file.FullName);
                }
            }
        }
    }
}