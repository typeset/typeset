using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Typeset.Web.Configuration;

namespace Typeset.Web.Controllers.Api
{
    public class RepositoryController : BaseApiController
    {
        public RepositoryController(IConfigurationManager configurationManager)
            : base(configurationManager)
        {
        }

        [HttpGet]
        public HttpResponseMessage Update(string token)
        {
            var siteRepositoryPullToken = ConfigurationManager.AppSettings["SiteRepository_UpdateToken"];

            if (token.Equals(siteRepositoryPullToken))
            {
                Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            Typeset.Domain.Git.Git.Pull(SitePath);
                        }
                        catch { }
                    });   
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}