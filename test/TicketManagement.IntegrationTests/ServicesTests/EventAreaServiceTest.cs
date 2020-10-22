using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.IntegrationTests.Factories;

namespace TicketManagement.IntegrationTests.ServicesTests
{
    [TestFixture]
    internal class EventAreaServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenInvoked_ReturnEventAreaDtos()
        {
            // Arrange
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            var eventAreas = await eventAreaService.GetAllAsync();

            // Assert
            eventAreas.Should().BeEquivalentTo(new List<EventAreaDto>
            {
                new EventAreaDto { Id = 1, EventId = 1, Description = "First area of layout 1", CoordX = 1, CoordY = 1, Price = 4.3m },
                new EventAreaDto { Id = 2, EventId = 1, Description = "Second area of layout 1", CoordX = 1, CoordY = 5, Price = 2.5m },
            });
        }

        [Test]
        public async Task GetByIdAsync_WhenExists_ReturnEventAreaDto()
        {
            // Arrange
            var eventAreaService = EventAreaFactory.MakeService();
            var id = 2;

            // Act
            var eventArea = await eventAreaService.GetByIdAsync(id);

            // Assert
            eventArea.Should().BeEquivalentTo(new EventAreaDto
            { Id = 2, EventId = 1, Description = "Second area of layout 1", CoordX = 1, CoordY = 5, Price = 2.5m });
        }

        [Test]
        public async Task UpdateAsync_WhenInvoked_ReturnUpdatedEventAreaDto()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var eventAreaService = EventAreaFactory.MakeService();
                var eventAreaDto = new EventAreaDto { Id = 2, EventId = -6, Description = "area 0", CoordX = 8, CoordY = 8, Price = 2.1m };

                // Act
                var returned = await eventAreaService.UpdateAsync(eventAreaDto);

                // Assert
                returned.Should().BeEquivalentTo(new EventAreaDto { Id = 2, EventId = 1, Description = "area 0", CoordX = 1, CoordY = 5, Price = 2.1m });
            }
        }
    }
}
