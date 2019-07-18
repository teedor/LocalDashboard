using DashboardServices;
using LocalDashboard.Web.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalDashboard.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashBoardService;
        private readonly IHttpContextWrapper _httpContextWrapper;

        public HomeController(IDashboardService dashBoardService, IHttpContextWrapper httpContextWrapper)
        {
            _dashBoardService = dashBoardService;
            _httpContextWrapper = httpContextWrapper;
        }

        public ActionResult Index()
        {
            var ipAddress = _httpContextWrapper.IpAddress == "::1" ? "62.31.103.154" : _httpContextWrapper.IpAddress;
            var model = _dashBoardService.GetDashboardModel(ipAddress);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}