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
            var ipStackDetails = _ipStackConnector.GetIpStackDetails(ipAddress);
            var isNorthKorea = ipStackDetails.CountryCode == "KP";
            var timeZoneDbDetails = _timeZoneDbConnector.GetTimeZoneDbDetails(ipStackDetails.Latitude, ipStackDetails.Longitude);
            var openWeatherMapDetails = _openWeatherMapConnector.GetOpenWeatherMapDetails(ipStackDetails.Latitude, ipStackDetails.Longitude, timeZoneDbDetails.GmtOffset);
            var newsArticles = !isNorthKorea
                ? _newsApiOrgConnector.GetNewsArticles(ipStackDetails.CountryCode, timeZoneDbDetails.GmtOffset)
                : null;
            var isTheSunUp = _dateHelper.IsTheSunUp(timeZoneDbDetails.LocalTime, openWeatherMapDetails.SunRiseTime, openWeatherMapDetails.SunSetTime);

            var result = new DashboardModel
            {
                LocalTime = timeZoneDbDetails.LocalTime,
                Temperature = openWeatherMapDetails.Temperature,
                WeatherDescription = openWeatherMapDetails.Description,
                NewsMessage = isNorthKorea ? "It would be pointless to display the news because it would be censored propaganda anyway." : string.Empty,
                WeatherMessage = isTheSunUp ? string.Empty : "The sun has set so the risk of shark attack is higher",
                NewsSummaries = newsArticles != null
                    ? newsArticles.Select(x => new NewsSummary
                        {
                            Description = x.Description,
                            Source = x.Source,
                            Title = x.Title,
                            Url = x.Url,
                            PublishedDateLocalTime = $"{x.PublishedDateLocalTime:yyyy-MM-dd HH:mm:ss}"
                        }).ToList()
                    : null
            };

            return result;
        }
    }
}