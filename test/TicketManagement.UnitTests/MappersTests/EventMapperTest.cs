using System;
using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Mappers;
using TicketManagement.DataAccess.Models;

namespace TicketManagement.UnitTests.MappersTests
{
    [TestFixture]
    internal class EventMapperTest
    {
        private readonly EventDto _dtoEquivalent = new EventDto
        {
            Id = 3,
            Name = "some name",
            Description = "some description",
            LayoutId = 2,
            StartDateTime = new DateTime(2020, 8, 25, 13, 45, 00),
            FinishDateTime = new DateTime(2020, 10, 25, 9, 15, 00),
        };

        private readonly EventDomain _domainEquivalent = new EventDomain
        {
            Id = 3,
            Name = "some name",
            Description = "some description",
            LayoutId = 2,
            StartDateTime = new DateTime(2020, 8, 25, 13, 45, 00),
            FinishDateTime = new DateTime(2020, 10, 25, 9, 15, 00),
        };

        [Test]
        public void GetEventDomain_WhenGiveEquivalent_ReturnTheSameEvent()
        {
            // Arrange
            var expected = _domainEquivalent;
            var ev = _dtoEquivalent;

            // Act
            var actual = EventMapper.GetEventDomain(ev);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetEventDto_WhenGiveEquivalent_ReturnTheSameEvent()
        {
            // Arrange
            var expected = _dtoEquivalent;
            var ev = _domainEquivalent;

            // Act
            var actual = EventMapper.GetEventDto(ev);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
