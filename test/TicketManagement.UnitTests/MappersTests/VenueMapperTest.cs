using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.UnitTests.MappersTests
{
    [TestFixture]
    internal class VenueMapperTest
    {
        private readonly VenueDto _dtoEquivalent = new VenueDto
        {
            Id = 3,
            Description = "some description",
            Address = "some address",
            Phone = "some phone",
        };

        private readonly VenueDomain _domainEquivalent = new VenueDomain
        {
            Id = 3,
            Description = "some description",
            Address = "some address",
            Phone = "some phone",
        };

        [Test]
        public void GetAreaDomain_WhenGiveEquivalent_ReturnTheSameArea()
        {
            // Arrange
            var expected = _domainEquivalent;
            var venue = _dtoEquivalent;

            // Act
            var actual = VenueMapper.GetVenueDomain(venue);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAreaDto_WhenGiveEquivalent_ReturnTheSameArea()
        {
            // Arrange
            var expected = _dtoEquivalent;
            var venue = _domainEquivalent;

            // Act
            var actual = VenueMapper.GetVenueDto(venue);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}