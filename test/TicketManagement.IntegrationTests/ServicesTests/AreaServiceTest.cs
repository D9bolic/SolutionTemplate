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
    internal class AreaServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenInvoked_ReturnAreaDtoList()
        {
            // Arrange
            var areaService = AreaFactory.MakeService();

            // Act
            var areas = await areaService.GetAllAsync();

            // Assert
            areas.Should().BeEquivalentTo(new List<AreaDto>
            {
                new AreaDto { Id = 1, LayoutId = 1, Description = "First area of layout 1", CoordX = 1, CoordY = 1 },
                new AreaDto { Id = 2, LayoutId = 1, Description = "Second area of layout 1", CoordX = 1, CoordY = 5 },
                new AreaDto { Id = 3, LayoutId = 2, Description = "First area of layout 2", CoordX = 1, CoordY = 2 },
                new AreaDto { Id = 4, LayoutId = 2, Description = "Second area of layout 2", CoordX = 4, CoordY = 3 },
            });
        }

        [Test]
        public async Task GetByIdAsync_WhenExists_ReturnAreaDto()
        {
            // Arrange
            var areaService = AreaFactory.MakeService();
            var id = 2;

            // Act
            var area = await areaService.GetByIdAsync(id);

            // Assert
            area.Should().BeEquivalentTo(new AreaDto { Id = 2, LayoutId = 1, Description = "Second area of layout 1", CoordX = 1, CoordY = 5 });
        }

        [Test]
        public async Task CreateAsync_WhenInvoked_ReturnAreaDto()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var areaService = AreaFactory.MakeService();
                var areaDto = new AreaDto { Id = 1, LayoutId = 1, Description = "area 0", CoordX = 3, CoordY = 3 };

                // Act
                var area = await areaService.CreateAsync(areaDto);

                // Assert
                area.Should().BeEquivalentTo(new AreaDto { Id = area.Id, LayoutId = 1, Description = "area 0", CoordX = 3, CoordY = 3 });
            }
        }

        [Test]
        public async Task CreateAsync_WhenAreaCreated_ThenCreatedAreaExistsInDB()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var areaService = AreaFactory.MakeService();
                var areaDto = new AreaDto { Id = 1, LayoutId = 1, Description = "area 0", CoordX = 3, CoordY = 3 };
                var listBefore = (await areaService.GetAllAsync()).ToList();

                // Act
                var area = await areaService.CreateAsync(areaDto);
                listBefore.Add(area);
                var listAfter = await areaService.GetAllAsync();

                // Assert
                listAfter.Should().BeEquivalentTo(listBefore);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenAreaDeleted_ThenDeletedAreaNotExistsInDB()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var areaService = AreaFactory.MakeService();
                var id = 2;

                // Act
                await areaService.DeleteAsync(id);
                AsyncTestDelegate action = async () => await areaService.GetByIdAsync(id);

                // Assert
                Assert.ThrowsAsync<ItemExistenceException>(action);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenAreaDeleted_AllConnectedSeatsShouldBeDeleted()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var areaService = AreaFactory.MakeService();
                var seatService = SeatFactory.MakeService();
                var id = 1;

                // Act
                await areaService.DeleteAsync(id);
                var list = await seatService.GetAllAsync();
                var idList = list.Select(seat => seat.AreaId);

                // Assert
                idList.Should().NotContain(id);
            }
        }

        [Test]
        public async Task UpdateAsync_WhenInvoked_ReturnUpdatedAreaDto()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var areaService = AreaFactory.MakeService();
                var areaDto = new AreaDto { Id = 1, LayoutId = 1, Description = "area 0", CoordX = 3, CoordY = 3 };

                // Act
                var returned = await areaService.UpdateAsync(areaDto);

                // Assert
                returned.Should().BeEquivalentTo(await areaService.GetByIdAsync(returned.Id));
            }
        }
    }
}