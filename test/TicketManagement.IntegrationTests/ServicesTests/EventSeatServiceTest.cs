using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.DataAccess.Enums;
using TicketManagement.IntegrationTests.Factories;

namespace TicketManagement.IntegrationTests.ServicesTests
{
    [TestFixture]
    internal class EventSeatServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenInvoked_ReturnEventSeatDtos()
        {
            // Arrange
            var eventSeatService = EventSeatFactory.MakeService();

            // Act
            var eventSeats = await eventSeatService.GetAllAsync();

            // Assert
            eventSeats.Should().BeEquivalentTo(new List<EventSeatDto>
            {
                new EventSeatDto { Id = 1, EventAreaId = 1, Row = 1, Number = 1, State = EventSeatState.Ordered },
                new EventSeatDto { Id = 2, EventAreaId = 1, Row = 1, Number = 2, State = EventSeatState.Free },
                new EventSeatDto { Id = 3, EventAreaId = 1, Row = 1, Number = 3, State = EventSeatState.Free },
                new EventSeatDto { Id = 4, EventAreaId = 1, Row = 2, Number = 2, State = EventSeatState.Free },
                new EventSeatDto { Id = 5, EventAreaId = 1, Row = 2, Number = 1, State = EventSeatState.Free },

                new EventSeatDto { Id = 6, EventAreaId = 2, Row = 1, Number = 1, State = EventSeatState.Ordered },
                new EventSeatDto { Id = 7, EventAreaId = 2, Row = 1, Number = 2, State = EventSeatState.Ordered },
                new EventSeatDto { Id = 8, EventAreaId = 2, Row = 1, Number = 3, State = EventSeatState.Inaccessible },
                new EventSeatDto { Id = 9, EventAreaId = 2, Row = 2, Number = 2, State = EventSeatState.Free },
                new EventSeatDto { Id = 10, EventAreaId = 2, Row = 2, Number = 1, State = EventSeatState.Free },
            });
        }

        [Test]
        public async Task GetByIdAsync_WhenExists_ReturnEventSeatDto()
        {
            // Arrange
            var eventSeatService = EventSeatFactory.MakeService();
            var id = 2;

            // Act
            var eventSeat = await eventSeatService.GetByIdAsync(id);

            // Assert
            eventSeat.Should().BeEquivalentTo(new EventSeatDto { Id = 2, EventAreaId = 1, Row = 1, Number = 2, State = EventSeatState.Free });
        }

        [Test]
        public async Task UpdateAsync_WhenInvoked_ReturnUpdatedEventSeatDto()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var eventSeatService = EventSeatFactory.MakeService();
                var eventSeatDto = new EventSeatDto { Id = 2, EventAreaId = 0, Row = 0, Number = 0, State = EventSeatState.Inaccessible };

                // Act
                var returned = await eventSeatService.UpdateAsync(eventSeatDto);

                // Assert
                returned.Should().BeEquivalentTo(new EventSeatDto { Id = 2, EventAreaId = 1, Row = 1, Number = 2, State = EventSeatState.Inaccessible });
            }
        }
    }
}