using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using TicketManagement.DataAccess.Enums;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.UnitTests.MappersTests
{
    [TestFixture]
    internal class EventSeatMapperTest
    {
        private readonly EventSeatDto _dtoEquivalent = new EventSeatDto
        {
            Id = 3,
            EventAreaId = 5,
            Row = 2,
            Number = 3,
            State = EventSeatState.Free,
        };

        private readonly EventSeatDomain _domainEquivalent = new EventSeatDomain
        {
            Id = 3,
            EventAreaId = 5,
            Row = 2,
            Number = 3,
            State = EventSeatState.Free,
        };

        [Test]
        public void GetEventSeatDomain_WhenGiveEquivalent_ReturnTheSameEventSeat()
        {
            // Arrange
            var expected = _domainEquivalent;
            var eventSeat = _dtoEquivalent;

            // Act
            var actual = EventSeatMapper.GetEventSeatDomain(eventSeat);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventSeatDto_WhenGiveEquivalent_ReturnTheSameEventSeat()
        {
            // Arrange
            var expected = _dtoEquivalent;
            var eventSeat = _domainEquivalent;

            // Act
            var actual = EventSeatMapper.GetEventSeatDto(eventSeat);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}