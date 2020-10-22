using System;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.Extentions;

namespace TicketManagement.UnitTests.ExtentionsTest
{
    [TestFixture]
    internal class DateTimeExtentionTest
    {
        [Test]
        public void IsBetween_WhenDateIsBetween_ReturnTrue()
        {
            // Arrange
            var current = new DateTime(2020, 08, 09, 10, 24, 43);
            var lowerLimit = new DateTime(2020, 08, 08, 10, 24, 43);
            var upperLimit = new DateTime(2020, 08, 10, 10, 24, 43);
            var expected = true;

            // Act
            var actual = current.IsBetween(lowerLimit, upperLimit);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsBetween_WhenDateIsBeforeLowerLimit_ReturnFalse()
        {
            // Arrange
            var current = new DateTime(2020, 08, 09, 10, 24, 43);
            var lowerLimit = new DateTime(2020, 08, 09, 11, 24, 43);
            var upperLimit = new DateTime(2020, 08, 10, 10, 24, 43);
            var expected = false;

            // Act
            var actual = current.IsBetween(lowerLimit, upperLimit);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsBetween_WhenDateIsAfterUpperLimit_ReturnFalse()
        {
            // Arrange
            var current = new DateTime(2020, 08, 09, 10, 24, 43);
            var lowerLimit = new DateTime(2020, 08, 08, 10, 24, 43);
            var upperLimit = new DateTime(2020, 08, 9, 9, 24, 43);
            var expected = false;

            // Act
            var actual = current.IsBetween(lowerLimit, upperLimit);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
