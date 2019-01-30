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
            var model = _dashBoardService.GetDashboardModel(_httpContextWrapper.IpAddress);
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