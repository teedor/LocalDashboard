using Connectors.IpStack;
using Connectors.NewsApiOrg;
using Connectors.OpenWeatherMap;
using Connectors.TimeZoneDb;
using DashboardServices;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDashbord.UnitTests
{
    [TestFixture]
    public class DashboardServiceTests
    {
        private IIpStackConnector _ipStackConnector;
        private INewsApiOrgConnector _newsApiOrgConnector;
        private IOpenWeatherMapConnector _openWeatherMapConnector;
        private ITimeZoneDbConnector _timeZoneDbConnector;
        private DashboardService _dashboardService;

        [SetUp]
        public void SetUp()
        {
            _ipStackConnector = Substitute.For<IIpStackConnector>();
            _newsApiOrgConnector = Substitute.For<INewsApiOrgConnector>();
            _openWeatherMapConnector = Substitute.For<IOpenWeatherMapConnector>();
            _timeZoneDbConnector = Substitute.For<ITimeZoneDbConnector>();
            _dashboardService = new DashboardService(_ipStackConnector, _newsApiOrgConnector, _openWeatherMapConnector, _timeZoneDbConnector);
        }

        [Test]
        public void GetDashboardModel_WhenCalled_ReturnsDashboardModel()
        {
            // Arrange
            var ipStackDetails = new IpStackDetails
            {
                CountryCode = "GB",
                Latitude = "5.2",
                Longitude = "-2.4"
            };

            _ipStackConnector.GetIpStackDetails(Arg.Any<string>()).Returns(ipStackDetails);

            var localTime = new DateTime(2019, 2, 1, 13, 45, 23);
            var timeZoneDbDetails = new TimeZoneDbDetails
            {
                GmtOffset = 0,
                LocalTime = localTime
            };

            _timeZoneDbConnector.GetTimeZoneDbDetails(Arg.Any<string>(), Arg.Any<string>()).Returns(timeZoneDbDetails);

            var openWeatherMapDetails = new OpenWeatherMapDetails
            {
                SunRiseTime = localTime.AddHours(-1),
                SunSetTime = localTime.AddHours(-1),
                Description = "Rain",
                Temperature = 23
            };

            _openWeatherMapConnector.GetOpenWeatherMapDetails(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>()).Returns(openWeatherMapDetails);

            var newsArticles = new List<NewsArticle>
            {
                new NewsArticle
                {
                    Source = "source 1",
                    Url = "Url 1",
                    Title = "Title 1",
                    Description = "Desc 1",
                    PublishedDateLocalTime = new DateTime(2019, 1, 1)
                }
            };

            _newsApiOrgConnector.GetNewsArticles(Arg.Any<string>(), Arg.Any<int>()).Returns(newsArticles);

            // Act
            var result = _dashboardService.GetDashboardModel("1.1.1.1");

            // Assert
            _ipStackConnector.Received().GetIpStackDetails("1.1.1.1");
            _timeZoneDbConnector.Received().GetTimeZoneDbDetails(ipStackDetails.Latitude, ipStackDetails.Longitude);
            _openWeatherMapConnector.Received().GetOpenWeatherMapDetails(ipStackDetails.Latitude, ipStackDetails.Longitude, timeZoneDbDetails.GmtOffset);
            _newsApiOrgConnector.Received().GetNewsArticles(ipStackDetails.CountryCode, timeZoneDbDetails.GmtOffset);

            Assert.AreEqual(result.LocalTime, timeZoneDbDetails.LocalTime);
            Assert.AreEqual(result.WeatherDescription, openWeatherMapDetails.Description);
            Assert.AreEqual(result.Temperature, openWeatherMapDetails.Temperature);

            Assert.AreEqual(result.NewsSummaries.First().Source, newsArticles.First().Source);
            Assert.AreEqual(result.NewsSummaries.First().Url, newsArticles.First().Url);
            Assert.AreEqual(result.NewsSummaries.First().Title, newsArticles.First().Title);
            Assert.AreEqual(result.NewsSummaries.First().Description, newsArticles.First().Description);
            Assert.AreEqual($"{result.NewsSummaries.First().PublishedDateLocalTime:yyyy-MM-dd HH:MM:ss}", newsArticles.First().PublishedDateLocalTime);
        }
    }
}
