using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.UnitTests.Factories;

namespace TicketManagement.UnitTests.ServicesTests
{
    [TestFixture]
    internal class SeatServiceTest
    {
        [Test]
        public void CreateAsync_WhenCreatableItemIsNull_ThrowException()
        {
            // Arrange
            SeatDto seatDto = null;
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.CreateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenRowIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 1, AreaId = 1, Number = 4, Row = 0 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.CreateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenNumberIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 1, AreaId = 1, Number = 0, Row = 4 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.CreateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenNumberAndRowAreNotUniqueForArea_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 1, AreaId = 1, Number = 1, Row = 1 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.CreateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<UniquePositionException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenAreaIdDoesNotExist_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 1, AreaId = 5, Number = 2, Row = 3 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.CreateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task CreateAsync_WhenAllIsOK_ThenReturnCreatedSeat()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 1, AreaId = 2, Number = 2, Row = 3 };
            var seatService = SeatFactory.MakeService();

            // Act
            var created = await seatService.CreateAsync(seatDto);

            // Assert
            created.Should().BeEquivalentTo(new SeatDto { Id = 4, AreaId = 2, Number = 2, Row = 3 });
        }

        [Test]
        public void DeleteAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var id = 0;
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void DeleteAsync_WhenIdDoesNotExist_ThrowException()
        {
            // Arrange
            var id = 11;
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetAllAsync_WhenAllIsOK_ReturnAllSeatsInDtoList()
        {
            // Arrange
            var seatService = SeatFactory.MakeService();

            // Act
            var result = await seatService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(new List<SeatDto>
            {
                new SeatDto { Id = 1, AreaId = 1, Number = 1, Row = 1 },
                new SeatDto { Id = 2, AreaId = 1, Number = 2, Row = 1 },
                new SeatDto { Id = 3, AreaId = 2, Number = 1, Row = 1 },
            });
        }

        [Test]
        public void GetByIdAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var id = 0;
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void GetByIdAsync_WhenIdDoesNotExist_ThrowException()
        {
            // Arrange
            var id = 11;
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetByIdAsync_WhenAllIsOK_ReturnSeatDto()
        {
            // Arrange
            var id = 2;
            var seatService = SeatFactory.MakeService();

            // Act
            var seat = await seatService.GetByIdAsync(id);

            // Assert
            seat.Should().BeEquivalentTo(new SeatDto { Id = 2, AreaId = 1, Number = 2, Row = 1 });
        }

        [Test]
        public void UpdateAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 0, AreaId = 1, Number = 4, Row = 0 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.UpdateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdDoesNotExist_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 11, AreaId = 1, Number = 4, Row = 2 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.UpdateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenRowIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 1, AreaId = 1, Number = 4, Row = 0 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.UpdateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenNumberIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 1, AreaId = 1, Number = 0, Row = 4 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.UpdateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenNumberAndRowAreNotUniqueForArea_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 2, AreaId = 2, Number = 1, Row = 1 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.UpdateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<UniquePositionException>(testAction);
        }

        [Test]
        public async Task UpdateAsync_WhenNumberAndRowAreUniqueForAreaBecauseIdISTheSameAndAllIsOK_ThenReturnUpatedSeat()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 3, AreaId = 2, Number = 1, Row = 1 };
            var seatService = SeatFactory.MakeService();

            // Act
            var updated = await seatService.UpdateAsync(seatDto);

            // Assert
            updated.Should().BeEquivalentTo(new SeatDto { Id = 3, AreaId = 2, Number = 1, Row = 1 });
        }

        [Test]
        public void UpdateAsync_WhenAreaIdDoesNotExist_ThrowException()
        {
            // Arrange
            var seatDto = new SeatDto { Id = 1, AreaId = 5, Number = 2, Row = 3 };
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.UpdateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenUpdatableItemIsNull_ThrowException()
        {
            // Arrange
            SeatDto seatDto = null;
            var seatService = SeatFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await seatService.UpdateAsync(seatDto);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }
    }
}