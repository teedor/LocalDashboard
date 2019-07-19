using Connectors.IpStack;
using Connectors.NewsApiOrg;
using Connectors.OpenWeatherMap;
using Connectors.TimeZoneDb;
using HelperClasses;
using System.Linq;

namespace DashboardServices
{
    public class DashboardService : IDashboardService
    {
        private readonly IIpStackConnector _ipStackConnector;
        public DashboardService(IIpStackConnector ipStackConnector)
        {
            _ipStackConnector = ipStackConnector;
        }

        public DashboardModel GetDashboardModel(string ipAddress)
        {
            var ipStackDetails = _ipStackConnector.GetIpStackDetails(ipAddress);

            return new DashboardModel
            {
                IpAddress = ipAddress,
                CountryCode = ipStackDetails.CountryCode,
                Latitude = ipStackDetails.Latitude,
                Longitude = ipStackDetails.Longitude
            };
        }
    }
}