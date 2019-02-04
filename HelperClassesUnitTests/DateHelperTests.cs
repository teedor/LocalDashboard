using HelperClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperClassesUnitTests
{
    [TestFixture]
    public class DateHelperTests
    {
        [Test]
        public void UnixIntToDateTimeTest()
        {
            // Arrange
            var unixDateInteger = 1549022518;

            // Act
            var result = new DateHelper().UnixIntToDateTime(unixDateInteger);

            // Assert
            Assert.AreEqual(new DateTime(2019, 2, 1, 12, 1, 58), result);
        }

        [Test]
        public void IsTheSunUpTest([ValueSource("_testData")]TestData testData)
        {
            // Act
            var result = new DateHelper().IsTheSunUp(testData.LocalTime, testData.Sunrise, testData.Sunset);

            // Assert
            Assert.AreEqual(testData.ExpectedResult, result);
        }

        public class TestData
        {
            public DateTime Sunrise { get; set; }
            public DateTime Sunset { get; set; }
            public DateTime LocalTime { get; set; }
            public bool ExpectedResult { get; set; }
        }

        private static TestData[] _testData = new[]
        {
            new TestData
            {
                LocalTime = new DateTime(2019, 1, 1, 12, 0, 0),
                Sunrise = new DateTime(2019, 1, 1, 6, 0, 0),
                Sunset = new DateTime(2019, 1, 1, 18, 0, 0),
                ExpectedResult = true
            },
            new TestData
            {
                LocalTime = new DateTime(2019, 1, 1, 5, 0, 0),
                Sunrise = new DateTime(2019, 1, 1, 6, 0, 0),
                Sunset = new DateTime(2019, 1, 1, 18, 0, 0),
                ExpectedResult = false
            },
            new TestData
            {
                LocalTime = new DateTime(2019, 1, 1, 19, 0, 0),
                Sunrise = new DateTime(2019, 1, 1, 6, 0, 0),
                Sunset = new DateTime(2019, 1, 1, 18, 0, 0),
                ExpectedResult = false
            }
        };
    }
}
