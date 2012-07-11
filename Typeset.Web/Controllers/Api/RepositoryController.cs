using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Typeset.Domain.Common;
using Typeset.Web.Configuration;

namespace Typeset.Web.Controllers.Api
{
    public class RepositoryController : BaseApiController
    {
        private IConfigurationManager ConfigurationManager { get; set; }

        public RepositoryController(IConfigurationManager configurationManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configuraitonManager");
            }

            ConfigurationManager = configurationManager;
        }

        public HttpResponseMessage Get(int limit = SearchCriteria.DefaultLimit, int offset = SearchCriteria.DefaultOffset, string order = "descending")
        {
            Task.Factory.StartNew(() =>
                {
                    Typeset.Domain.Git.Git.Pull(ConfigurationManager.AppSettings[""]);
                });
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}