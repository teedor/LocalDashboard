using Connectors.IpStack;
using Connectors.NewsApiOrg;
using Connectors.OpenWeatherMap;
using Connectors.TimeZoneDb;
using DashboardServices;
using HelperClasses;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDashboard.UnitTests
{
    [TestFixture]
    public class DashboardServiceTests
    {
        private ISpecialRulesEngine _specialRulesEngine;
        private IIpStackConnector _ipStackConnector;
        private ITimeZoneDbConnector _timeZoneDbConnector;
        private IOpenWeatherMapConnector _openWeatherMapConnector;
        private INewsApiOrgConnector _newsApiOrgConnector;
        private DashboardService _dashboardService;

        [SetUp]
        public void SetUp()
        {
            _specialRulesEngine = Substitute.For<ISpecialRulesEngine>();
            _ipStackConnector = Substitute.For<IIpStackConnector>();
            _timeZoneDbConnector = Substitute.For<ITimeZoneDbConnector>();
            _openWeatherMapConnector = Substitute.For<IOpenWeatherMapConnector>();
            _newsApiOrgConnector = Substitute.For<INewsApiOrgConnector>();
            _dashboardService = new DashboardService(_ipStackConnector, _specialRulesEngine, _timeZoneDbConnector, _openWeatherMapConnector, _newsApiOrgConnector);
        }

        [Test]
        public void GetDashboardModel_WhenCalled_ReturnsDashboardModel()
        {
            // Arrange
            const string ipAddress = "1.2.3.4";

            // mocked ip stack details
            var ipStackDetails = new IpStackDetails
            {
                CountryCode = "Bob",
                Latitude = "23.554",
                Longitude = "4.65"
            };

            // mocked timezoneDbDetails
            var timezoneDbDetails = new TimeZoneDbDetails
            {
                GmtOffset = 1200,
                LocalTime = new DateTime(2019, 7, 2, 3, 4, 5)
            };

            // mocked weather details
            var openweatherMapDetails = new OpenWeatherMapDetails
            {
                Description = "Rainy",
                SunRiseTime = new DateTime(2019, 1, 1),
                SunSetTime = new DateTime(2019, 1, 1),
                Temperature = 17.4m
            };

            // mocked news
            var newsArticles = new List<NewsArticle>
            {
                new NewsArticle()
            };

            _ipStackConnector.GetIpStackDetails(Arg.Any<string>()).Returns(ipStackDetails);
            _timeZoneDbConnector.GetTimeZoneDbDetails(Arg.Any<string>(), Arg.Any<string>()).Returns(timezoneDbDetails);
            _newsApiOrgConnector.GetNewsArticles(Arg.Any<string>(), Arg.Any<int>()).Returns(newsArticles);
            _openWeatherMapConnector.GetOpenWeatherMapDetails(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>())
                .Returns(openweatherMapDetails);

            // Act
            var result = _dashboardService.GetDashboardModel(ipAddress);

            // Assert
            Assert.AreEqual(ipAddress, result.IpAddress);
            Assert.AreEqual(ipStackDetails.CountryCode, result.CountryCode);
            Assert.AreEqual(ipStackDetails.Latitude, result.Latitude);
            Assert.AreEqual(ipStackDetails.Longitude, result.Longitude);
            Assert.AreEqual(timezoneDbDetails.LocalTime, result.LocalTime);
            Assert.AreEqual(newsArticles, result.NewsArticles);
            Assert.AreEqual(openweatherMapDetails.Description, result.WeatherDescription);
            Assert.AreEqual(openweatherMapDetails.Temperature, result.Temperature);
            Assert.AreEqual(openweatherMapDetails.SunRiseTime, result.Sunrise);
            Assert.AreEqual(openweatherMapDetails.SunSetTime, result.Sunset);

            _ipStackConnector.Received().GetIpStackDetails(ipAddress);
            _specialRulesEngine.Received().ApplyRules(Arg.Is<DashboardModel>(x => x.CountryCode == ipStackDetails.CountryCode));
            _timeZoneDbConnector.Received().GetTimeZoneDbDetails(ipStackDetails.Latitude, ipStackDetails.Longitude);
            _newsApiOrgConnector.Received().GetNewsArticles(ipStackDetails.CountryCode, timezoneDbDetails.GmtOffset);
            _openWeatherMapConnector.Received().GetOpenWeatherMapDetails(ipStackDetails.Latitude, ipStackDetails.Longitude, timezoneDbDetails.GmtOffset);
        }

        [Test]
        public void GetDashboardModel_IpStackFails_ShowMessage()
        {
            // Arrange
            const string ipAddress = "1.2.3.4";

            _ipStackConnector.When(x => x.GetIpStackDetails(Arg.Any<string>()))
                .Do(x => throw new ApplicationException("this connection is shit"));

            // Act
            var result = _dashboardService.GetDashboardModel(ipAddress);

            // Assert
            Assert.IsTrue(result.SpecialMessages.Any(x => x == "The IP Stack Connector Failed so no dashboard today"));

            _ipStackConnector.Received().GetIpStackDetails(ipAddress);
        }
    }
}
