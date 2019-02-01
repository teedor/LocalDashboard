using Connectors.IpStack;
using Connectors.NewsApiOrg;
using Connectors.OpenWeatherMap;
using Connectors.TimeZoneDb;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardServices
{
    public class DashboardSettingsWrapper : IIpStackConnectorSettings, IOpenWeatherMapConnectorSettings, ITimeZoneDbConnectorSettings, INewsApiOrgConnectorSettings
    {
        public string IpStackAccessKey => ConfigurationManager.AppSettings["IpStackAccessKey"];

        public string OpenWeatherMapApiKey => ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];

        public string TimeZoneDbApiKey => ConfigurationManager.AppSettings["TimeZoneDbApiKey"];

        public string NewsApiOrgApiKey => ConfigurationManager.AppSettings["NewsApiOrgApiKey"];
    }
}
