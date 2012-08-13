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
            var appDataVirtualPath = "~/App_Data";
            var appDataAbsolutePath = context.Request.MapPath("~/App_Data");
            var virtualDirectoryPath = "~/App_Data/remote-resources/";
            var absoluteDirectoryPath = context.Request.MapPath("~/App_Data/remote-resources");

            foreach (var path in paths.Select(p => p.TrimStart('/').Replace('/', '\\')))
            {
                try
                {
                    var sourceFileName = System.IO.Path.Combine(sitePath, path);
                    if (sourceFileName.StartsWith(appDataAbsolutePath))
                    {
                        var virtualFileName = string.Concat(sourceFileName.Replace(appDataAbsolutePath, appDataVirtualPath), path).Replace('\\', '/');
                        virtualPaths.Add(virtualFileName);
                    }
                    else
                    {
                        var virtualFileName = System.IO.Path.Combine(virtualDirectoryPath, path.Replace('\\', '/'));
                        var destinationFileName = System.IO.Path.Combine(absoluteDirectoryPath, path);
                        var destinationDirectory = System.IO.Path.GetDirectoryName(destinationFileName);
                        virtualFileName = virtualFileName.Replace('\\', '/');

                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }

                        File.Copy(sourceFileName, destinationFileName, true);
                        virtualPaths.Add(virtualFileName);
                    }
                }
                catch { }
            }

            return base.Include(virtualPaths.ToArray());
        }
    }
}