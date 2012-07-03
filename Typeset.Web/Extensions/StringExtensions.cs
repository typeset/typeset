using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Typeset.Domain.Common;

namespace Typeset.Web.Extensions
{
    public static class StringExtensions
    {
        public static string GetMimeType(this string path)
        {
            var mimeType = "application/octet-stream";

            try
            {
                var extension = Path.GetExtension(path);
                extension = extension.ToLower();

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        mimeType = "image/jpg";
                        break;
                    case ".gif":
                        mimeType = "image/gif";
                        break;
                    case ".png":
                        mimeType = "image/png";
                        break;
                    case ".ico":
                        mimeType = "image/x-icon";
                        break;
                    case ".css":
                        mimeType = "text/css";
                        break;
                    case ".js":
                        mimeType = "text/javascript";
                        break;
                    case ".htm":
                    case ".html":
                        mimeType = "text/html";
                        break;
                    case ".txt":
                        mimeType = "text/plain";
                        break;
                    default:
                        if(FrontMatter.FrontMatterExtensions.Any(ext => extension.Equals(ext)))
                        {
                            mimeType = "text/plain";
                        }
                        break;
                }
            }
            catch { }

            return mimeType;
        }
    }
}