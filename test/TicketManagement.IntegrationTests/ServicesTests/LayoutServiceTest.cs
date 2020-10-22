using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.IntegrationTests.Factories;

namespace TicketManagement.IntegrationTests.ServicesTests
{
    [TestFixture]
    internal class LayoutServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenInvoked_ReturnLayoutDtos()
        {
            // Arrange
            var layoutService = LayoutFactory.MakeService();

            // Act
            var layouts = await layoutService.GetAllAsync();

            // Assert
            layouts.Should().BeEquivalentTo(new List<LayoutDto>
            {
                new LayoutDto { Id = 1, VenueId = 1, Description = "layout 1" },
                new LayoutDto { Id = 2, VenueId = 1, Description = "layout 2" },
            });
        }

        [Test]
        public async Task GetByIdAsync_WhenExists_ReturnLayoutDto()
        {
            // Arrange
            var layoutservice = LayoutFactory.MakeService();
            var id = 2;

            // Act
            var layout = await layoutservice.GetByIdAsync(id);

            // Assert
            layout.Should().BeEquivalentTo(new LayoutDto { Id = 2, VenueId = 1, Description = "layout 2" });
        }

        [Test]
        public async Task CreateAsync_WhenInvoked_ReturnVenueDto()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var layoutService = LayoutFactory.MakeService();
                var layoutDto = new LayoutDto { Id = 1, VenueId = 1, Description = "layout 0" };

                // Act
                var layout = await layoutService.CreateAsync(layoutDto);

                // Assert
                layout.Should().BeEquivalentTo(new LayoutDto { Id = layout.Id, VenueId = 1, Description = "layout 0" });
            }
        }

        [Test]
        public async Task CreateAsync_WhenLayoutCreated_ThenCreatedLayoutExistsInDB()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var layoutService = LayoutFactory.MakeService();
                var layoutDto = new LayoutDto { Id = 6, VenueId = 1, Description = "layout 0" };
                var listBefore = (await layoutService.GetAllAsync()).ToList();

                // Act
                var layout = await layoutService.CreateAsync(layoutDto);
                listBefore.Add(layout);
                var listAfter = await layoutService.GetAllAsync();

                // Assert
                listAfter.Should().BeEquivalentTo(listBefore);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenLayoutDeleted_ThenLayoutNotExistInDB()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var layoutService = LayoutFactory.MakeService();
                var id = 2;

                // Act
                await layoutService.DeleteAsync(id);
                AsyncTestDelegate action = async () => await layoutService.GetByIdAsync(id);

                // Assert
                Assert.ThrowsAsync<ItemExistenceException>(action);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenLayoutDeleted_AllConnectedAreasShouldBeDeleted()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var layoutService = LayoutFactory.MakeService();
                var areaService = AreaFactory.MakeService();

                var id = 1;

                // Act
                await layoutService.DeleteAsync(id);
                var list = await areaService.GetAllAsync();
                var idList = list.Select(area => area.LayoutId);

                // Assert
                idList.Should().NotContain(id);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenLayoutDeleted_AllConnectedEventsShouldBeDeleted()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var layoutService = LayoutFactory.MakeService();
                var eventService = EventFactory.MakeService();

                var id = 1;

                // Act
                await layoutService.DeleteAsync(id);
                var list = await eventService.GetAllAsync();
                var idList = list.Select(ev => ev.LayoutId);

                // Assert
                idList.Should().NotContain(id);
            }
        }

        [Test]
        public async Task UpdateAsync_WhenInvoked_ReturnUpdatedLayoutDto()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var layoutService = LayoutFactory.MakeService();
                var layoutDto = new LayoutDto { Id = 2, VenueId = 1, Description = "layout 0" };

                // Act
                var returned = await layoutService.UpdateAsync(layoutDto);

                // Assert
                returned.Should().BeEquivalentTo(await layoutService.GetByIdAsync(returned.Id));
            }
        }
    }
}