using System.Collections.Generic;
using Moq;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.UnitTests.Factories
{
    internal static class EventAreaFactory
    {
        public static EventAreaService MakeService()
        {
            // create repository
            var eventAreaRepositiry = new Mock<IEventAreaRepository>();

            // mock repository(GetAllAsync method)
            eventAreaRepositiry
                .Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<EventAreaDomain>
                {
                    new EventAreaDomain { Id = 1, EventId = 1, Description = "description 1", CoordX = -1, CoordY = 4, Price = 2.5m },
                    new EventAreaDomain { Id = 2, EventId = 1, Description = "description 2", CoordX = 1, CoordY = 4, Price = 4.5m },
                    new EventAreaDomain { Id = 3, EventId = 2, Description = "description 3", CoordX = 5, CoordY = 2, Price = 3m },
                });

            // mock GetByIdAsync method
            eventAreaRepositiry
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new EventAreaDomain { Id = 2, EventId = 1, Description = "description 2", CoordX = 1, CoordY = 4, Price = 4.5m });

            // mock update and insert methods
            eventAreaRepositiry
                .Setup(repository => repository.UpdateAsync(It.IsAny<EventAreaDomain>()))
                .ReturnsAsync(new EventAreaDomain { Id = 1, EventId = 2, Description = "description 1", CoordX = 6, CoordY = 6, Price = 2.3m });

            return new EventAreaService(eventAreaRepositiry.Object);
        }
    }
}