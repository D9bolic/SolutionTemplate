using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.UnitTests.MappersTests
{
    [TestFixture]
    internal class SeatMapperTest
    {
        private readonly SeatDto _dtoEquivalent = new SeatDto
        {
            Id = 3,
            AreaId = 5,
            Row = 2,
            Number = 3,
        };

        private readonly SeatDomain _domainEquivalent = new SeatDomain
        {
            Id = 3,
            AreaId = 5,
            Row = 2,
            Number = 3,
        };

        [Test]
        public void GetSeatDomain_WhenGiveEquivalent_ReturnTheSameSeat()
        {
            // Arrange
            var expected = _domainEquivalent;
            var seat = _dtoEquivalent;

            // Act
            var actual = SeatMapper.GetSeatDomain(seat);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetSeatDto_WhenGiveEquivalent_ReturnTheSameSeat()
        {
            // Arrange
            var expected = _dtoEquivalent;
            var seat = _domainEquivalent;

            // Act
            var actual = SeatMapper.GetSeatDto(seat);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}