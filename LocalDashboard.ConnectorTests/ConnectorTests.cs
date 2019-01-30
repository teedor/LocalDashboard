﻿using DashboardServices;
using Connectors.IpStack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var ipStackConnector = new IpStackConnector(settings);

            // Act
            var result = ipStackConnector.GetIpStackResponse("185.69.144.1");

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
