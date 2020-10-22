using System.Collections.Generic;
using Moq;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.UnitTests.Factories
{
    internal static class SeatFactory
    {
        public static SeatService MakeService()
        {
            // create repositories
            var areaRepository = new Mock<IAreaRepository>();
            var seatRepository = new Mock<ISeatRepository>();

            // mock repositories(GetAllAsync method)
            areaRepository
                .Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<AreaDomain>
                {
                    new AreaDomain { Id = 1, LayoutId = 1, Description = "some description 1", CoordX = 1, CoordY = -1 },
                    new AreaDomain { Id = 2, LayoutId = 1, Description = "some description 2", CoordX = -1, CoordY = 1 },
                    new AreaDomain { Id = 3, LayoutId = 2, Description = "some description 3", CoordX = 1, CoordY = 1 },
                });
            seatRepository
                 .Setup(repository => repository.GetAllAsync())
                 .ReturnsAsync(new List<SeatDomain>
                 {
                    new SeatDomain { Id = 1, AreaId = 1, Number = 1, Row = 1 },
                    new SeatDomain { Id = 2, AreaId = 1, Number = 2, Row = 1 },
                    new SeatDomain { Id = 3, AreaId = 2, Number = 1, Row = 1 },
                 });

            // mock GetByIdAsync method
            seatRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new SeatDomain { Id = 2, AreaId = 1, Number = 2, Row = 1 });

            // mock update and insert methods
            seatRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<SeatDomain>()))
                .ReturnsAsync(new SeatDomain { Id = 3, AreaId = 2, Number = 1, Row = 1 });
            seatRepository
                .Setup(repository => repository.CreateAsync(It.IsAny<SeatDomain>()))
                .ReturnsAsync(new SeatDomain { Id = 4, AreaId = 2, Number = 2, Row = 3 });

            return new SeatService(seatRepository.Object, areaRepository.Object);
        }
    }
}