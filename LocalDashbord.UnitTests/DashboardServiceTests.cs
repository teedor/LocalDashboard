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
        private DashboardService _dashboardService;

        [SetUp]
        public void SetUp()
        {
            _ipStackConnector = Substitute.For<IIpStackConnector>();
            _dashboardService = new DashboardService(_ipStackConnector);
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

            _ipStackConnector.GetIpStackDetails(Arg.Any<string>()).Returns(ipStackDetails);

            // Act
            var result = _dashboardService.GetDashboardModel(ipAddress);

            // Assert
            Assert.AreEqual(ipAddress, result.IpAddress);
            Assert.AreEqual(ipStackDetails.CountryCode, result.CountryCode);
            Assert.AreEqual(ipStackDetails.Latitude, result.Latitude);
            Assert.AreEqual(ipStackDetails.Longitude, result.Longitude);

            _ipStackConnector.Received(1).GetIpStackDetails(ipAddress);
        }
    }
}
