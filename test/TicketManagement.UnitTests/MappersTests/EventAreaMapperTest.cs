using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.UnitTests.MappersTests
{
    [TestFixture]
    internal class EventAreaMapperTest
    {
        private readonly EventAreaDto _dtoEquivalent = new EventAreaDto
        {
            Id = 3,
            Description = "some description",
            EventId = 2,
            CoordX = 12,
            CoordY = 123,
            Price = 2.37m,
        };

        private readonly EventAreaDomain _domainEquivalent = new EventAreaDomain
        {
            Id = 3,
            Description = "some description",
            EventId = 2,
            CoordX = 12,
            CoordY = 123,
            Price = 2.37m,
        };

        [Test]
        public void GetEventAreaDomain_WhenGiveEquivalent_ReturnTheSameEventArea()
        {
            // Arrange
            var expected = _domainEquivalent;
            var eventArea = _dtoEquivalent;

            // Act
            var actual = EventAreaMapper.GetEventAreaDomain(eventArea);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventAreaDto_WhenGiveEquivalent_ReturnTheSameEventArea()
        {
            // Arrange
            var expected = _dtoEquivalent;
            var eventArea = _domainEquivalent;

            // Act
            var actual = EventAreaMapper.GetEventAreaDto(eventArea);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
