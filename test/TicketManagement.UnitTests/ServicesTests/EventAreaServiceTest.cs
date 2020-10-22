using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using TicketManagement.UnitTests.Factories;

namespace TicketManagement.UnitTests.ServicesTests
{
    [TestFixture]
    internal class EventAreaServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenAllIsOK_ReturnAllEventAreasInDtoList()
        {
            // Arrange
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            var result = await eventAreaService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(new List<EventAreaDto>
            {
                new EventAreaDto { Id = 1, EventId = 1, Description = "description 1", CoordX = -1, CoordY = 4, Price = 2.5m },
                new EventAreaDto { Id = 2, EventId = 1, Description = "description 2", CoordX = 1, CoordY = 4, Price = 4.5m },
                new EventAreaDto { Id = 3, EventId = 2, Description = "description 3", CoordX = 5, CoordY = 2, Price = 3m },
            });
        }

        [Test]
        public void GetByIdAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var id = 0;
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventAreaService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void GetByIdAsync_WhenIdNotExists_ThrowException()
        {
            // Arrange
            var id = 5;
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventAreaService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetByIdAsync_WhenAllIsOK_ReturnEventAreaDto()
        {
            // Arrange
            var id = 1;
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            var result = await eventAreaService.GetByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(new EventAreaDto { Id = 2, EventId = 1, Description = "description 2", CoordX = 1, CoordY = 4, Price = 4.5m });
        }

        [Test]
        public void UpdateAsync_WhenPriceLessThanZero_ThrowException()
        {
            // Arrange
            var eventArea = new EventAreaDto { Id = 1, EventId = 2, Description = "description 0", CoordX = 1, CoordY = 2, Price = -2.3m };
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventAreaService.UpdateAsync(eventArea);

            // Assert
            Assert.ThrowsAsync<NegativePriceException>(testAction);
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void UpdateAsync_WhenDescriptionIsNullOrWhiteSpace_ThrowException(string description)
        {
            // Arrange
            var eventArea = new EventAreaDto { Id = 1, EventId = 2, Description = description, CoordX = 1, CoordY = 2, Price = 2.3m };
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventAreaService.UpdateAsync(eventArea);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhendescriptionIsNotUniqueForEvent_ThrowException()
        {
            // Arrange
            var eventArea = new EventAreaDto { Id = 2, EventId = 1, Description = "description 1", CoordX = 1, CoordY = 2, Price = 2.3m };
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventAreaService.UpdateAsync(eventArea);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var eventArea = new EventAreaDto { Id = 0, EventId = 1, Description = "description 0", CoordX = 1, CoordY = 2, Price = 2.3m };
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventAreaService.UpdateAsync(eventArea);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdNotExists_ThrowException()
        {
            // Arrange
            var eventArea = new EventAreaDto { Id = 20, EventId = 1, Description = "description 0", CoordX = 1, CoordY = 2, Price = 2.3m };
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventAreaService.UpdateAsync(eventArea);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task UpdateAsync_WhenDescriptionIsUniqueForEventBecauseIdIsTheSameAndAllIsOk_ThenReturnUpdatedEventArea()
        {
            // Arrange
            var eventArea = new EventAreaDto { Id = 1, EventId = 1, Description = "description 1", CoordX = 1, CoordY = 2, Price = 2.3m };
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            var updated = await eventAreaService.UpdateAsync(eventArea);

            // Assert
            updated.Should().BeEquivalentTo(new EventAreaDto { Id = 1, EventId = 2, Description = "description 1", CoordX = 6, CoordY = 6, Price = 2.3m });
        }

        [Test]
        public void UpdateAsync_WhenUpdatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            EventAreaDto eventArea = null;
            var eventAreaService = EventAreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await eventAreaService.UpdateAsync(eventArea);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }
    }
}