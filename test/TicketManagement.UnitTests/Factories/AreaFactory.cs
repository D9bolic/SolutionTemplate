using System.Collections.Generic;
using Moq;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.UnitTests.Factories
{
    internal static class AreaFactory
    {
        public static AreaService MakeService()
        {
            // create repositories
            var areaRepository = new Mock<IAreaRepository>();
            var layoutRepository = new Mock<ILayoutRepository>();

            // mock repositories(GetAllAsync method)
            areaRepository
                .Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<AreaDomain>
                {
                    new AreaDomain { Id = 1, LayoutId = 1, Description = "some description 1", CoordX = 1, CoordY = -1 },
                    new AreaDomain { Id = 2, LayoutId = 1, Description = "some description 2", CoordX = -1, CoordY = 1 },
                    new AreaDomain { Id = 3, LayoutId = 2, Description = "some description 3", CoordX = 1, CoordY = 1 },
                });
            layoutRepository
                 .Setup(repository => repository.GetAllAsync())
                 .ReturnsAsync(new List<LayoutDomain>
                 {
                    new LayoutDomain { Id = 1, VenueId = 1, Description = "description 1" },
                    new LayoutDomain { Id = 2, VenueId = 1, Description = "description 2" },
                    new LayoutDomain { Id = 3, VenueId = 2, Description = "description 3" },
                 });

            // mock GetByIdAsync method
            areaRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new AreaDomain { Id = 3, LayoutId = 2, Description = "some description 3", CoordX = 1, CoordY = 1 });

            // mock update and insert methods
            areaRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<AreaDomain>()))
                .ReturnsAsync(new AreaDomain { Id = 2, LayoutId = 1, Description = "some description 2", CoordX = -1, CoordY = -3 });
            areaRepository
                .Setup(repository => repository.CreateAsync(It.IsAny<AreaDomain>()))
                .ReturnsAsync(new AreaDomain { Id = 1, LayoutId = 1, Description = "some description 10", CoordX = -1, CoordY = -3 });

            return new AreaService(areaRepository.Object, layoutRepository.Object);
        }
    }
}
