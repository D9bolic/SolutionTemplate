using System.Collections.Generic;
using Moq;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Enums;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.UnitTests.Factories
{
    internal static class EventSeatFactory
    {
        public static EventSeatService MakeService()
        {
            // create repository
            var eventSeatRepositiry = new Mock<IEventSeatRepository>();

            // mock repository(GetAllAsync method)
            eventSeatRepositiry
                .Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<EventSeatDomain>
                {
                    new EventSeatDomain { Id = 1, EventAreaId = 1, Row = 1, Number = 1, State = EventSeatState.Free },
                    new EventSeatDomain { Id = 2, EventAreaId = 1, Row = 1, Number = 2, State = EventSeatState.Free },
                    new EventSeatDomain { Id = 3, EventAreaId = 2, Row = 1, Number = 1, State = EventSeatState.Engaged },
                });

            // mock GetByIdAsync method
            eventSeatRepositiry
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new EventSeatDomain { Id = 2, EventAreaId = 1, Row = 1, Number = 2, State = EventSeatState.Free });

            // mock update and insert methods
            eventSeatRepositiry
                .Setup(repository => repository.UpdateAsync(It.IsAny<EventSeatDomain>()))
                .ReturnsAsync(new EventSeatDomain { Id = 2, EventAreaId = 3, Row = 2, Number = 5, State = EventSeatState.Engaged });

            return new EventSeatService(eventSeatRepositiry.Object);
        }
    }
}