using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Typeset.Domain.Repository;
using Typeset.Web.Configuration;

namespace Typeset.Web.Controllers.Api
{
    public class RepositoryController : BaseApiController
    {
        private IRepositoryManager RepositoryManager { get; set; }

        public RepositoryController(IConfigurationManager configurationManager,
            IRepositoryManager repositoryManager)
            : base(configurationManager)
        {
            if (repositoryManager == null)
            {
                throw new ArgumentNullException("repositoryManager");
            }

            RepositoryManager = repositoryManager;
        }

        [HttpPost]
        public HttpResponseMessage Update(string token)
        {
            try
            {
                var adminToken = ConfigurationManager.AppSettings["AdminToken"];
                var siteRepositoryUri = ConfigurationManager.AppSettings["SiteRepositoryUri"];
                var sitePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["AppData_Site_Path"]);

                if (token.Equals(adminToken))
                {
                    Task.Factory.StartNew(() => RepositoryManager.CheckoutOrUpdate(siteRepositoryUri, sitePath));
                }
            }
            catch { }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}