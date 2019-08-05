using System;
using System.Collections.Generic;
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
        private readonly ISpecialRulesEngine _specialRulesEngine;
        private readonly ITimeZoneDbConnector _timeZoneDbConnector;
        private readonly IOpenWeatherMapConnector _openWeatherMapConnector;
        private readonly INewsApiOrgConnector _newsApiOrgConnector;

        public DashboardService(IIpStackConnector ipStackConnector, ISpecialRulesEngine specialRulesEngine, ITimeZoneDbConnector timeZoneDbConnector, IOpenWeatherMapConnector openWeatherMapConnector, INewsApiOrgConnector newsApiOrgConnector)
        {
            _specialRulesEngine = specialRulesEngine;
            _timeZoneDbConnector = timeZoneDbConnector;
            _openWeatherMapConnector = openWeatherMapConnector;
            _newsApiOrgConnector = newsApiOrgConnector;
            _ipStackConnector = ipStackConnector;
        }

        public DashboardModel GetDashboardModel(string ipAddress)
        {
            DashboardModel dashboardModel = new DashboardModel();

            try
            {
                // get all the data from the APIS
                var ipStackDetails = _ipStackConnector.GetIpStackDetails(ipAddress);
                var timezoneDbDetails = _timeZoneDbConnector.GetTimeZoneDbDetails(ipStackDetails.Latitude, ipStackDetails.Longitude);
                var openWeatherMapDetails = _openWeatherMapConnector.GetOpenWeatherMapDetails(ipStackDetails.Latitude, ipStackDetails.Longitude, timezoneDbDetails.GmtOffset);
                var newsArticles = _newsApiOrgConnector.GetNewsArticles(ipStackDetails.CountryCode, timezoneDbDetails.GmtOffset);

                // build the dashboardModel from the aquired data
                dashboardModel = new DashboardModel
                {
                    IpAddress = ipAddress,
                    CountryCode = ipStackDetails.CountryCode,
                    Latitude = ipStackDetails.Latitude,
                    Longitude = ipStackDetails.Longitude,
                    LocalTime = timezoneDbDetails.LocalTime,
                    WeatherDescription = openWeatherMapDetails.Description,
                    NewsArticles = newsArticles,
                    Sunrise = openWeatherMapDetails.SunRiseTime,
                    Sunset = openWeatherMapDetails.SunSetTime,
                    Temperature = openWeatherMapDetails.Temperature
                };

                // Apply the busness rules
                _specialRulesEngine.ApplyRules(dashboardModel);
            }
            catch (ApplicationException)
            {
                return new DashboardModel
                {
                    SpecialMessages = new List<string>
                    {
                        "The IP Stack Connector Failed so no dashboard today"
                    }
                };
            }

            return dashboardModel;
        }
    }
}