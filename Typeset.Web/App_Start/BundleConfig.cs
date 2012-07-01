using System.Web;
using System.Web.Optimization;

namespace Typeset.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var cssBundle = new Bundle("~/Content/css/all", new CssMinify());
            cssBundle.IncludeDirectory("~/App_Data/content/css", "*.css", true);
            bundles.Add(cssBundle);

            var jsBundle = new Bundle("~/scripts/all", new JsMinify());
            jsBundle.IncludeDirectory("~/App_Data/content/js", "*.js", true);
            bundles.Add(jsBundle);
            
            BundleTable.EnableOptimizations = true;
        }
    }
}