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
        private DashboardService _dashboardService;

        [SetUp]
        public void SetUp()
        {
            _dashboardService = new DashboardService();
        }

        [Test]
        public void GetDashboardModel_WhenCalled_ReturnsDashboardModel()
        {
            // Arrange

            // Act

            // Assert

        }

        [Test]
        public void GetDashboardModel_WhenInNorthKorea_ReturnsDashboardModelWithNoNewsMessage()
        {
            // Arrange

            // Act

            // Assert

        }

        [Test]
        public void GetDashboardModel_SunIsDown_DisplaySharkWarning()
        {
            // Arrange

            // Act

            // Assert

        }
    }
}
