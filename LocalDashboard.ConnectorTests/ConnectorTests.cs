using DashboardServices;
using Connectors.IpStack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connectors.OpenWeatherMap;
using HelperClasses;
using Connectors.TimeZoneDb;
using Connectors.NewsApiOrg;

namespace LocalDashboard.ConnectorTests
{
    [TestFixture]
    public class ConnectorTests
    {
        [Test]
        public void IpStackConnectorTest()
        {
            // Arrange
            var settings = new DashboardSettingsWrapper();
            var ipStackConnector = new IpStackConnector(settings);

            // Act
            var result = ipStackConnector.GetIpStackResponse("185.69.144.1");

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void OpenWeatherMapConnectorTest()
        {
            // Arrange
            var settings = new DashboardSettingsWrapper();
            var dateHelper = new DateHelper();
            var openWeatherMapConnector = new OpenWeatherMapConnector(settings, dateHelper);

            // Act
            var result = openWeatherMapConnector.GetOpenWeatherMapDetails("15", "2", 3600);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TimeZoneDbConnectorTest()
        {
            // Arrange
            var dateHelper = new DateHelper();
            var settings = new DashboardSettingsWrapper();
            var timeZoneDbConnector = new TimeZoneDbConnector(dateHelper, settings);

            // Act
            var result = timeZoneDbConnector.GetTimeZoneDbDetails("15", "2");

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void NewsApiOrgConnectorTest()
        {
            // Arrange
            var settings = new DashboardSettingsWrapper();
            var newsApiOrgConnector = new NewsApiOrgConnector(settings);

            // Act
            var result = newsApiOrgConnector.GetNewsArticles("GB", 0);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
