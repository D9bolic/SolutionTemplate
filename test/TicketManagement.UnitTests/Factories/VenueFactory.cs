using System.Collections.Generic;
using Moq;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Models;
using TicketManagement.DataAccess.Repositories.Interfaces;

namespace TicketManagement.UnitTests.Factories
{
    internal static class VenueFactory
    {
        public static VenueService MakeService()
        {
            // create repository
            var venueRepository = new Mock<IVenueRepository>();

            // mock repository(GetAllAsync method)
            venueRepository
                .Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(new List<VenueDomain>
                 {
                    new VenueDomain { Id = 1, Address = "some address 1", Description = "some description 1", Phone = "123456789011" },
                    new VenueDomain { Id = 2, Address = "some address 2", Description = "some description 2", Phone = "123456789012" },
                    new VenueDomain { Id = 3, Address = "some address 3", Description = "some description 3", Phone = "123456789013" },
                 });

            // mock GetByIdAsync method
            venueRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new VenueDomain { Id = 1, Address = "addres 1", Description = "unique 1", Phone = "123456789012" });

            // mock update and insert methods
            venueRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<VenueDomain>()))
                .ReturnsAsync(new VenueDomain { Id = 2, Address = "some address", Description = "some description 2", Phone = "123456789012" });
            venueRepository
                .Setup(repository => repository.CreateAsync(It.IsAny<VenueDomain>()))
                .ReturnsAsync(new VenueDomain { Id = 4, Address = "some address 4", Description = "unique 3", Phone = "123456789012" });

            return new VenueService(venueRepository.Object);
        }
    }
}
