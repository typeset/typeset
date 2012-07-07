using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Typeset.Web.Less.BundleTransforms
{
    public class LessCompile : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var lessFiles = response.Files.Where(f => f.Extension.Equals(".less", StringComparison.OrdinalIgnoreCase));
            foreach (var file in lessFiles)
            {
                using (var reader = new StreamReader(file.FullName))
                {
                    var less = reader.ReadToEnd();
                    var compiledLess = Less.Compiler.Compile(less);
                    reader.Close();
                    response.Content = response.Content.Replace(less, compiledLess);
                }
            }
            response.ContentType = "text/css";
            response.Cacheability = HttpCacheability.Public;
        }
    }
}