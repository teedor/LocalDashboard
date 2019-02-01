using Connectors.IpStack;
using Connectors.NewsApiOrg;
using Connectors.OpenWeatherMap;
using Connectors.TimeZoneDb;
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

        public DashboardService(IIpStackConnector ipStackConnector, INewsApiOrgConnector newsApiOrgConnector, IOpenWeatherMapConnector openWeatherMapConnector, ITimeZoneDbConnector timeZoneDbConnector)
        {
            _ipStackConnector = ipStackConnector;
            _newsApiOrgConnector = newsApiOrgConnector;
            _openWeatherMapConnector = openWeatherMapConnector;
            _timeZoneDbConnector = timeZoneDbConnector;
        }

        public DashboardModel GetDashboardModel(string ipAddress)
        {
            return new DashboardModel { LocalTime = DateTime.Now };
        }
    }
}
