using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.UnitTests.MappersTests
{
    [TestFixture]
    internal class LayoutMapperTest
    {
        private readonly LayoutDto _dtoEquivalent = new LayoutDto
        {
            Id = 3,
            Description = "some description",
            VenueId = 2,
        };

        private readonly LayoutDomain _domainEquivalent = new LayoutDomain
        {
            Id = 3,
            Description = "some description",
            VenueId = 2,
        };

        [Test]
        public void GetLayoutDomain_WhenGiveEquivalen_ReturnTheSameLayout()
        {
            // Arrange
            var expected = _domainEquivalent;
            var layout = _dtoEquivalent;

            // Act
            var actual = LayoutMapper.GetLayoutDomain(layout);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetLayoutDto_WhenGiveEquivalent_ReturnTheSameLayout()
        {
            // Arrange
            var expected = _dtoEquivalent;
            var layout = _domainEquivalent;

            // Act
            var actual = LayoutMapper.GetLayoutDto(layout);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
