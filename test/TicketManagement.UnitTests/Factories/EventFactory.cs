using System;
using System.Collections.Generic;
using Moq;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.UnitTests.Factories
{
    internal static class EventFactory
    {
        public static EventService MakeService(DateTime dateTime)
        {
            // create repositories
            var eventRepository = new Mock<IEventRepository>();
            var areaRepository = new Mock<IAreaRepository>();
            var seatRepository = new Mock<ISeatRepository>();
            var layoutRepository = new Mock<ILayoutRepository>();

            // mock repositories(GetAllAsync method)
            eventRepository
                .Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<EventDomain>
                {
                    new EventDomain
                    {
                        Id = 1,
                        LayoutId = 1,
                        Name = "name 1",
                        Description = "description event 1",
                        StartDateTime = dateTime.AddDays(13),
                        FinishDateTime = dateTime.AddDays(15),
                    },
                    new EventDomain
                    {
                        Id = 2,
                        LayoutId = 1,
                        Name = "name 2",
                        Description = "description event 2",
                        StartDateTime = dateTime.AddDays(18),
                        FinishDateTime = dateTime.AddDays(18).AddHours(3),
                    },
                    new EventDomain
                    {
                        Id = 3,
                        LayoutId = 2,
                        Name = "name 3",
                        Description = "description event 3",
                        StartDateTime = dateTime.AddDays(21),
                        FinishDateTime = dateTime.AddDays(22),
                    },
                });
            areaRepository
                .Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<AreaDomain>
                {
                    new AreaDomain { Id = 1, LayoutId = 1, Description = "description 1", CoordX = 1, CoordY = -1 },
                    new AreaDomain { Id = 2, LayoutId = 1, Description = "description 2", CoordX = -1, CoordY = 1 },
                    new AreaDomain { Id = 3, LayoutId = 2, Description = "description 3", CoordX = 1, CoordY = 1 },
                });
            seatRepository
                 .Setup(repository => repository.GetAllAsync())
                 .ReturnsAsync(new List<SeatDomain>
                 {
                    new SeatDomain { Id = 1, AreaId = 1, Number = 1, Row = 1 },
                    new SeatDomain { Id = 2, AreaId = 2, Number = 2, Row = 1 },
                    new SeatDomain { Id = 3, AreaId = 3, Number = 1, Row = 1 },
                 });
            layoutRepository
                 .Setup(repository => repository.GetAllAsync())
                 .ReturnsAsync(new List<LayoutDomain>
                 {
                    new LayoutDomain { Id = 1, VenueId = 1, Description = "description 1" },
                    new LayoutDomain { Id = 2, VenueId = 1, Description = "description 2" },
                    new LayoutDomain { Id = 3, VenueId = 2, Description = "description 3" },
                 });

            // mock GetByIdAsync methods
            eventRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new EventDomain
                {
                    Id = 2,
                    LayoutId = 1,
                    Name = "name 2",
                    Description = "description event 2",
                    StartDateTime = new DateTime(2020, 08, 21, 21, 21, 30),
                    FinishDateTime = new DateTime(2020, 09, 21, 21, 21, 30),
                });
            layoutRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new LayoutDomain { Id = 2, VenueId = 1, Description = "description 2" });

            // mock update and insert methods
            eventRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<EventDomain>()))
                .ReturnsAsync(new EventDomain
                {
                    Id = 2,
                    LayoutId = 2,
                    Name = "name 0",
                    Description = "description event 2",
                    StartDateTime = dateTime.AddDays(2),
                    FinishDateTime = dateTime.AddDays(5),
                });
            eventRepository
                .Setup(repository => repository.CreateAsync(It.IsAny<EventDomain>(), It.IsAny<decimal>()))
                .ReturnsAsync(new EventDomain
                {
                    Id = 3,
                    LayoutId = 2,
                    Name = "name 0",
                    Description = "description 0",
                    StartDateTime = dateTime.AddDays(2),
                    FinishDateTime = dateTime.AddDays(5),
                });

            return new EventService(eventRepository.Object, areaRepository.Object, seatRepository.Object, layoutRepository.Object);
        }
    }
}