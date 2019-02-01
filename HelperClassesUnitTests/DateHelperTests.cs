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
    }

}
