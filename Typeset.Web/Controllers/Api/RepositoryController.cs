using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Typeset.Domain.Git;
using Typeset.Web.Configuration;

namespace Typeset.Web.Controllers.Api
{
    public class RepositoryController : BaseApiController
    {
        public RepositoryController(IConfigurationManager configurationManager)
            : base(configurationManager)
        {
        }

        [HttpPost]
        public HttpResponseMessage Update(string token)
        {
            try
            {
                var adminToken = ConfigurationManager.AppSettings["AdminToken"];
                var siteRepositoryUri = ConfigurationManager.AppSettings["SiteRepositoryUri"];
                var sitePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AppData_Site_Path"]);

                if (string.IsNullOrWhiteSpace(token) || !token.Equals(adminToken))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                if (string.IsNullOrWhiteSpace(siteRepositoryUri))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                if (siteRepositoryUri.EndsWith(".git", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (!Directory.EnumerateFileSystemEntries(sitePath).Any())
                    {
                        Task.Factory.StartNew(() =>
                        {
                            Git.Clone(siteRepositoryUri, sitePath);
                        });
                    }
                    else
                    {
                        Task.Factory.StartNew(() => 
                        {
                            Git.Pull(sitePath);
                        });
                    }
                }
            }
            catch { }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}