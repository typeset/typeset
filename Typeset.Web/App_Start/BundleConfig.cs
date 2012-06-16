using System.Web;
using System.Web.Optimization;

namespace Typeset.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var cssBundle = new Bundle("~/Content/css/all", new CssMinify());
            cssBundle.IncludeDirectory("~/Content", "*.css");
            bundles.Add(cssBundle);

            var jsBundle = new Bundle("~/scripts/all", new JsMinify());
            jsBundle.IncludeDirectory("~/Scripts", "*.js");
            bundles.Add(jsBundle);
        }
    }
}