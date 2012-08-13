using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace System.Web.Optimization
{
    public class TypesetBundle : Bundle
    {
        public TypesetBundle(string virtualPath)
            : base(virtualPath)
        {
        }

        public TypesetBundle(string virtualPath, params IBundleTransform[] transforms)
            : base(virtualPath, transforms)
        {
        }

        public Bundle IncludeLocalAndRemote(HttpContextBase context, string sitePath, params string[] paths)
        {
            var virtualPaths = new List<string>();
            var appDataPath = context.Request.MapPath("~/App_Data");

            foreach (var path in paths)
            {
                try
                {
                    if (sitePath.StartsWith(appDataPath, StringComparison.OrdinalIgnoreCase))
                    {
                        var virtualPath1 = sitePath.Replace(appDataPath, "~/App_Data").Replace('\\', '/');
                        var virtualPath2 = path;
                        virtualPaths.Add(string.Concat(virtualPath1, virtualPath2));
                    }
                    else
                    {
                        var sourcePath1 = sitePath;
                        var sourcePath2 = path.Replace('/', '\\').TrimStart('\\');
                        var sourceFileName = System.IO.Path.Combine(sourcePath1, sourcePath2);

                        var destinationPath1 = context.Request.MapPath("~/App_Data/remote-resources");
                        var destinationPath2 = path.Replace('/', '\\').TrimStart('\\');
                        var destinationFileName = System.IO.Path.Combine(destinationPath1, destinationPath2);

                        var destinationDirectory = System.IO.Path.GetDirectoryName(destinationFileName);
                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }

                        File.Copy(sourceFileName, destinationFileName, true);
                        virtualPaths.Add(string.Concat("~/App_Data/remote-resources", path));
                    }
                }
                catch { }
            }

            return base.Include(virtualPaths.ToArray());
        }
    }
}