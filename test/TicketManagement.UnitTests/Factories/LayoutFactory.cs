using System.Collections.Generic;
using Moq;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.UnitTests.Factories
{
    internal static class LayoutFactory
    {
        public static LayoutService MakeService()
        {
            // create repositories
            var venueRepository = new Mock<IVenueRepository>();
            var layoutRepository = new Mock<ILayoutRepository>();

            // mock repositories(GetAllAsync method)
            venueRepository
                .Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<VenueDomain>
                {
                    new VenueDomain { Id = 1, Address = "some address 1", Description = "some description 1", Phone = "123456789011" },
                    new VenueDomain { Id = 2, Address = "some address 2", Description = "some description 2", Phone = "123456789012" },
                    new VenueDomain { Id = 3, Address = "some address 3", Description = "some description 3", Phone = "123456789013" },
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
            layoutRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new LayoutDomain { Id = 3, VenueId = 2, Description = "some description 1" });

            // mock update and insert methods
            layoutRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<LayoutDomain>()))
                .ReturnsAsync(new LayoutDomain { Id = 2, VenueId = 2, Description = "description 1" });
            layoutRepository
                .Setup(repository => repository.CreateAsync(It.IsAny<LayoutDomain>()))
                .ReturnsAsync(new LayoutDomain { Id = 4, VenueId = 3, Description = "description 4" });

            return new LayoutService(layoutRepository.Object, venueRepository.Object);
        }
    }
}
