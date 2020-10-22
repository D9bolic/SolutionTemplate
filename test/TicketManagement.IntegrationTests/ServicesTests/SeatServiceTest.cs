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
    internal class SeatServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenInvoked_ReturnSeatDtos()
        {
            // Arrange
            var seatService = SeatFactory.MakeService();

            // Act
            var seats = await seatService.GetAllAsync();

            // Assert
            seats.Should().BeEquivalentTo(new List<SeatDto>
            {
                new SeatDto { Id = 1, AreaId = 1, Row = 1, Number = 1 },
                new SeatDto { Id = 2, AreaId = 1, Row = 1, Number = 2 },
                new SeatDto { Id = 3, AreaId = 1, Row = 1, Number = 3 },
                new SeatDto { Id = 4, AreaId = 1, Row = 2, Number = 2 },
                new SeatDto { Id = 5, AreaId = 1, Row = 2, Number = 1 },

                new SeatDto { Id = 6, AreaId = 2, Row = 1, Number = 1 },
                new SeatDto { Id = 7, AreaId = 2, Row = 1, Number = 2 },
                new SeatDto { Id = 8, AreaId = 2, Row = 1, Number = 3 },
                new SeatDto { Id = 9, AreaId = 2, Row = 2, Number = 2 },
                new SeatDto { Id = 10, AreaId = 2, Row = 2, Number = 1 },

                new SeatDto { Id = 11, AreaId = 3, Row = 1, Number = 1 },
                new SeatDto { Id = 12, AreaId = 3, Row = 1, Number = 2 },
                new SeatDto { Id = 13, AreaId = 3, Row = 1, Number = 3 },
                new SeatDto { Id = 14, AreaId = 3, Row = 2, Number = 2 },
                new SeatDto { Id = 15, AreaId = 3, Row = 2, Number = 1 },

                new SeatDto { Id = 16, AreaId = 4, Row = 1, Number = 1 },
                new SeatDto { Id = 17, AreaId = 4, Row = 1, Number = 2 },
                new SeatDto { Id = 18, AreaId = 4, Row = 1, Number = 3 },
                new SeatDto { Id = 19, AreaId = 4, Row = 2, Number = 2 },
                new SeatDto { Id = 20, AreaId = 4, Row = 2, Number = 1 },
            });
        }

        [Test]
        public async Task GetByIdAsync_WhenExists_ReturnSeatDto()
        {
            // Arrange
            var seatService = SeatFactory.MakeService();
            var id = 2;

            // Act
            var seat = await seatService.GetByIdAsync(id);

            // Assert
            seat.Should().BeEquivalentTo(new SeatDto { Id = 2, AreaId = 1, Row = 1, Number = 2 });
        }

        [Test]
        public async Task CreateAsync_WhenInvoked_ReturnSeatDto()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var seatService = SeatFactory.MakeService();
                var seatDto = new SeatDto { Id = 1, AreaId = 2, Row = 5, Number = 5 };

                // Act
                var seat = await seatService.CreateAsync(seatDto);

                // Assert
                seat.Should().BeEquivalentTo(new SeatDto { Id = seat.Id, AreaId = 2, Row = 5, Number = 5 });
            }
        }

        [Test]
        public async Task CreateAsync_WhenSeatCreated_ThenCreatedSeatExistsInDB()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var seatService = SeatFactory.MakeService();
                var seatDto = new SeatDto { Id = 1, AreaId = 2, Row = 5, Number = 5 };
                var listBefore = (await seatService.GetAllAsync()).ToList();

                // Act
                var seat = await seatService.CreateAsync(seatDto);
                listBefore.Add(seat);
                var listAfter = await seatService.GetAllAsync();

                // Assert
                listAfter.Should().BeEquivalentTo(listBefore);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenSeatDeleted_ThenDeletedSeatNotExistInDB()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var seatService = SeatFactory.MakeService();
                var id = 2;

                // Act
                await seatService.DeleteAsync(id);
                AsyncTestDelegate action = async () => await seatService.GetByIdAsync(id);

                // Assert
                Assert.ThrowsAsync<ItemExistenceException>(action);
            }
        }

        [Test]
        public async Task UpdateAsync_WhenInvoked_ReturnUpdatedSeatDto()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var seatService = SeatFactory.MakeService();
                var seatDto = new SeatDto { Id = 1, AreaId = 2, Row = 5, Number = 5 };

                // Act
                var returned = await seatService.UpdateAsync(seatDto);

                // Assert
                returned.Should().BeEquivalentTo(await seatService.GetByIdAsync(returned.Id));
            }
        }
    }
}