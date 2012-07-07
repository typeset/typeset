using System;
using System.IO;
using System.Web;
using System.Web.Optimization;

namespace Typeset.Web.Less.BundleTransforms
{
    public class LessCompile : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = string.Empty;
            response.ContentType = "text/css";
            response.Cacheability = HttpCacheability.Public;

            foreach (var file in response.Files)
            {
                var fileText = File.ReadAllText(file.FullName);
                if (file.Extension.Equals(".less", StringComparison.OrdinalIgnoreCase))
                {
                    response.Content += Less.Compiler.Compile(fileText);
                }
                else
                {
                    response.Content += fileText;
                }
            }
        }
    }
}