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
        [TestCase(1563548884, "2019-07-19 16:08:04")]
        [TestCase(1532528563, "2018-07-25 15:22:43")]
        public void UnixIntToDateTimeTest(int unixDateInteger, string expectedDateTime)
        {
            // Act
            var result = new DateHelper().UnixIntToDateTime(unixDateInteger);

            // Assert
            Assert.AreEqual(Convert.ToDateTime(expectedDateTime), result);
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
            },
            new TestData
            {
                LocalTime = new DateTime(2019, 1, 1, 18, 0, 0),
                Sunrise = new DateTime(2019, 1, 1, 6, 0, 0),
                Sunset = new DateTime(2019, 1, 1, 18, 0, 0),
                ExpectedResult = false
            },
            new TestData
            {
                LocalTime = new DateTime(2019, 1, 1, 6, 0, 0),
                Sunrise = new DateTime(2019, 1, 1, 6, 0, 0),
                Sunset = new DateTime(2019, 1, 1, 18, 0, 0),
                ExpectedResult = true
            }
        };
    }
}
