using Connectors.IpStack;
using Connectors.NewsApiOrg;
using Connectors.OpenWeatherMap;
using Connectors.TimeZoneDb;
using HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardServices
{
    public class DashboardService : IDashboardService
    {
        private IIpStackConnector _ipStackConnector;
        private INewsApiOrgConnector _newsApiOrgConnector;
        private IOpenWeatherMapConnector _openWeatherMapConnector;
        private ITimeZoneDbConnector _timeZoneDbConnector;
        private IDateHelper _dateHelper;

        public DashboardService(IIpStackConnector ipStackConnector, INewsApiOrgConnector newsApiOrgConnector, IOpenWeatherMapConnector openWeatherMapConnector, ITimeZoneDbConnector timeZoneDbConnector, IDateHelper dateHelper)
        {
            _ipStackConnector = ipStackConnector;
            _newsApiOrgConnector = newsApiOrgConnector;
            _openWeatherMapConnector = openWeatherMapConnector;
            _timeZoneDbConnector = timeZoneDbConnector;
            _dateHelper = dateHelper;
        }

        public DashboardModel GetDashboardModel(string ipAddress)
        {
            return new DashboardModel { LocalTime = DateTime.Now };
        }
    }
}
