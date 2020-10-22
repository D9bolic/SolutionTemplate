using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Enums;
using TicketManagement.DataAccess.Models;
using TicketManagement.UnitTests.Factories;

namespace TicketManagement.UnitTests.ServicesTests
{
    [TestFixture]
    internal class EventSeatServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenAllIsOK_ReturnAllEventSeatsInDtoList()
        {
            // Arrange
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            var result = await eventSeatService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(new List<EventSeatDto>
            {
                new EventSeatDto { Id = 1, EventAreaId = 1, Row = 1, Number = 1, State = EventSeatState.Free },
                new EventSeatDto { Id = 2, EventAreaId = 1, Row = 1, Number = 2, State = EventSeatState.Free },
                new EventSeatDto { Id = 3, EventAreaId = 2, Row = 1, Number = 1, State = EventSeatState.Engaged },
            });
        }

        [Test]
        public void GetByIdAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var id = 0;
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventSeatService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void GetByIdAsync_WhenIdNotExists_ThrowException()
        {
            // Arrange
            var id = 5;
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventSeatService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetByIdAsync_WhenAllIsOK_ReturnEventSeatDto()
        {
            // Arrange
            var id = 1;
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            var result = await eventSeatService.GetByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(new EventSeatDomain { Id = 2, EventAreaId = 1, Row = 1, Number = 2, State = EventSeatState.Free });
        }

        [Test]
        public void UpdateAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var eventSeat = new EventSeatDto { Id = 0, EventAreaId = 3, Row = 2, Number = 5, State = EventSeatState.Engaged };
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventSeatService.UpdateAsync(eventSeat);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdNotExists_ThrowException()
        {
            // Arrange
            var eventSeat = new EventSeatDto { Id = 6, EventAreaId = 3, Row = 2, Number = 5, State = EventSeatState.Engaged };
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventSeatService.UpdateAsync(eventSeat);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task UpdateAsync_WhenAllIsOK_ThenReturnUpdatedEventSeat()
        {
            // Arrange
            var eventSeat = new EventSeatDto { Id = 2, EventAreaId = 5, Row = 3, Number = 3, State = EventSeatState.Engaged };
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            var updated = await eventSeatService.UpdateAsync(eventSeat);

            // Assert
            updated.Should().BeEquivalentTo(new EventSeatDto { Id = 2, EventAreaId = 3, Row = 2, Number = 5, State = EventSeatState.Engaged });
        }

        [Test]
        public void UpdateAsync_WhenUpdatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            EventSeatDto eventSeat = null;
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventSeatService.UpdateAsync(eventSeat);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }
    }
}
