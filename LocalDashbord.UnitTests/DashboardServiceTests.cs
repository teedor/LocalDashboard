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
        private IIpStackConnector _ipStackConnector;
        private INewsApiOrgConnector _newsApiOrgConnector;
        private IOpenWeatherMapConnector _openWeatherMapConnector;
        private ITimeZoneDbConnector _timeZoneDbConnector;
        private DashboardService _dashboardService;
        private IDateHelper _dateHelper;

        [SetUp]
        public void SetUp()
        {
            _ipStackConnector = Substitute.For<IIpStackConnector>();
            _newsApiOrgConnector = Substitute.For<INewsApiOrgConnector>();
            _openWeatherMapConnector = Substitute.For<IOpenWeatherMapConnector>();
            _timeZoneDbConnector = Substitute.For<ITimeZoneDbConnector>();
            _dateHelper = Substitute.For<IDateHelper>();
            _dashboardService = new DashboardService(_ipStackConnector, _newsApiOrgConnector, _openWeatherMapConnector, _timeZoneDbConnector, _dateHelper);
        }

        [Test]
        public void GetDashboardModel_WhenCalled_ReturnsDashboardModel()
        {
            // Arrange
            var ipAddress = "1.1.1.1";

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

            _dateHelper.IsTheSunUp(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(true);

            // Act
            var result = _dashboardService.GetDashboardModel(ipAddress);

            // Assert
            _ipStackConnector.Received().GetIpStackDetails(ipAddress);
            _timeZoneDbConnector.Received().GetTimeZoneDbDetails(ipStackDetails.Latitude, ipStackDetails.Longitude);
            _openWeatherMapConnector.Received().GetOpenWeatherMapDetails(ipStackDetails.Latitude, ipStackDetails.Longitude, timeZoneDbDetails.GmtOffset);
            _newsApiOrgConnector.Received().GetNewsArticles(ipStackDetails.CountryCode, timeZoneDbDetails.GmtOffset);
            _dateHelper.Received().IsTheSunUp(timeZoneDbDetails.LocalTime, openWeatherMapDetails.SunRiseTime, openWeatherMapDetails.SunSetTime);

            Assert.AreEqual(ipAddress, result.IpAddress);
            Assert.AreEqual(timeZoneDbDetails.LocalTime, result.LocalTime);
            Assert.AreEqual(openWeatherMapDetails.Description, result.WeatherDescription);
            Assert.AreEqual(openWeatherMapDetails.Temperature, result.Temperature);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.NewsMessage));
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.WeatherMessage));

            Assert.AreEqual(newsArticles.First().Source, result.NewsSummaries.First().Source);
            Assert.AreEqual(newsArticles.First().Url, result.NewsSummaries.First().Url);
            Assert.AreEqual(newsArticles.First().Title, result.NewsSummaries.First().Title);
            Assert.AreEqual(newsArticles.First().Description, result.NewsSummaries.First().Description);
            Assert.AreEqual($"{newsArticles.First().PublishedDateLocalTime:yyyy-MM-dd HH:mm:ss}", result.NewsSummaries.First().PublishedDateLocalTime);
        }

        [Test]
        public void GetDashboardModel_WhenInNorthKorea_ReturnsDashboardModelWithNoNewsMessage()
        {
            // Arrange
            var ipAddress = "1.1.1.1";

            var ipStackDetails = new IpStackDetails
            {
                CountryCode = "KP",
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

            _dateHelper.IsTheSunUp(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(true);

            // Act
            var result = _dashboardService.GetDashboardModel(ipAddress);

            // Assert
            _ipStackConnector.Received().GetIpStackDetails(ipAddress);
            _timeZoneDbConnector.Received().GetTimeZoneDbDetails(ipStackDetails.Latitude, ipStackDetails.Longitude);
            _openWeatherMapConnector.Received().GetOpenWeatherMapDetails(ipStackDetails.Latitude, ipStackDetails.Longitude, timeZoneDbDetails.GmtOffset);
            _newsApiOrgConnector.DidNotReceive().GetNewsArticles(Arg.Any<string>(), Arg.Any<int>());
            _dateHelper.Received().IsTheSunUp(timeZoneDbDetails.LocalTime, openWeatherMapDetails.SunRiseTime, openWeatherMapDetails.SunSetTime);

            Assert.AreEqual(ipAddress, result.IpAddress);
            Assert.AreEqual(result.LocalTime, timeZoneDbDetails.LocalTime);
            Assert.AreEqual(result.WeatherDescription, openWeatherMapDetails.Description);
            Assert.AreEqual(result.Temperature, openWeatherMapDetails.Temperature);

            Assert.IsNull(result.NewsSummaries);

            Assert.AreEqual("It would be pointless to display the news because it would be censored propaganda anyway.", result.NewsMessage);
        }

        [Test]
        public void GetDashboardModel_SunIsDown_DisplaySharkWarning()
        {
            // Arrange
            var ipAddress = "1.1.1.1";

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

            _dateHelper.IsTheSunUp(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(false);

            // Act
            var result = _dashboardService.GetDashboardModel(ipAddress);

            // Assert
            _ipStackConnector.Received().GetIpStackDetails(ipAddress);
            _timeZoneDbConnector.Received().GetTimeZoneDbDetails(ipStackDetails.Latitude, ipStackDetails.Longitude);
            _openWeatherMapConnector.Received().GetOpenWeatherMapDetails(ipStackDetails.Latitude, ipStackDetails.Longitude, timeZoneDbDetails.GmtOffset);
            _newsApiOrgConnector.Received().GetNewsArticles(ipStackDetails.CountryCode, timeZoneDbDetails.GmtOffset);
            _dateHelper.Received().IsTheSunUp(timeZoneDbDetails.LocalTime, openWeatherMapDetails.SunRiseTime, openWeatherMapDetails.SunSetTime);

            Assert.AreEqual(ipAddress, result.IpAddress);
            Assert.AreEqual(timeZoneDbDetails.LocalTime, result.LocalTime);
            Assert.AreEqual(openWeatherMapDetails.Description, result.WeatherDescription);
            Assert.AreEqual(openWeatherMapDetails.Temperature, result.Temperature);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.NewsMessage));
            Assert.AreEqual("The sun has set so the risk of shark attack is higher", result.WeatherMessage);

            Assert.AreEqual(newsArticles.First().Source, result.NewsSummaries.First().Source);
            Assert.AreEqual(newsArticles.First().Url, result.NewsSummaries.First().Url);
            Assert.AreEqual(newsArticles.First().Title, result.NewsSummaries.First().Title);
            Assert.AreEqual(newsArticles.First().Description, result.NewsSummaries.First().Description);
            Assert.AreEqual($"{newsArticles.First().PublishedDateLocalTime:yyyy-MM-dd HH:mm:ss}", result.NewsSummaries.First().PublishedDateLocalTime);
        }
    }
}
