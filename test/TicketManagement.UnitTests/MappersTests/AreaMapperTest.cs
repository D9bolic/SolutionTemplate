using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.UnitTests.MappersTests
{
    [TestFixture]
    internal class AreaMapperTest
    {
        private readonly AreaDto _dtoEquivalent = new AreaDto
        {
            Id = 3,
            Description = "some description",
            LayoutId = 2,
            CoordX = 12,
            CoordY = 123,
        };

        private readonly AreaDomain _domainEquivalent = new AreaDomain
        {
            Id = 3,
            Description = "some description",
            LayoutId = 2,
            CoordX = 12,
            CoordY = 123,
        };

        [Test]
        public void GetAreaDomain_WhenGiveEquivalent_ReturnTheSameArea()
        {
            // Arrange
            var expected = _domainEquivalent;
            var area = _dtoEquivalent;

            // Act
            var actual = AreaMapper.GetAreaDomain(area);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAreaDto_WhenGiveEquivalent_ReturnTheSameArea()
        {
            // Arrange
            var expected = _dtoEquivalent;
            var area = _domainEquivalent;

            // Act
            var actual = AreaMapper.GetAreaDto(area);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}