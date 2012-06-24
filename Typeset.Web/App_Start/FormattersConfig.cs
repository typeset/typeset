using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Net.Http.Formatting;
using Typeset.Web.ViewResults;

namespace Typeset.Web
{
    public class FormattersConfig
    {
        public static void RegisterFormatters()
        {
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.FormUrlEncodedFormatter);
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
    }
}