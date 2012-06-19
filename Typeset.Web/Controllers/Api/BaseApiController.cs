using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Typeset.Web.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected const int DefaultLimit = 10;
        protected const int DefaultOffset = 0;
    }
}