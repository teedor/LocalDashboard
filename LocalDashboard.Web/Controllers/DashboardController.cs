using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LocalDashboard.Web.Wrappers;

namespace LocalDashboard.Web.Controllers
{
    public class DashboardController : ApiController
    {
        private readonly IHttpContextWrapper _httpContextWrapper;

        public DashboardController(IHttpContextWrapper httpContextWrapper)
        {
            _httpContextWrapper = httpContextWrapper;
        }

        public HttpResponseMessage GetDashboard()
        {
            var ip = _httpContextWrapper.IpAddress;
            return Request.CreateResponse(HttpStatusCode.OK, ip);
        }
    }
}
