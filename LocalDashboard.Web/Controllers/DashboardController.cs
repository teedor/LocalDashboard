using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DashboardServices;
using LocalDashboard.Web.Wrappers;

namespace LocalDashboard.Web.Controllers
{
    public class DashboardController : ApiController
    {
        private readonly IHttpContextWrapper _httpContextWrapper;
        private readonly IDashboardService _dashboardService;

        public DashboardController(IHttpContextWrapper httpContextWrapper, IDashboardService dashboardService)
        {
            _httpContextWrapper = httpContextWrapper;
            _dashboardService = dashboardService;
        }

        public HttpResponseMessage GetDashboard()
        {
            var ip = _httpContextWrapper.IpAddress;
            var dashboard = _dashboardService.GetDashboardModel(ip);
            return Request.CreateResponse(HttpStatusCode.OK, dashboard);
        }
    }
}
