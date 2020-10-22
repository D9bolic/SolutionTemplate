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
    internal class EventServiceTest
    {
        [Test]
        public void CreateAsync_WhenCreatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            EventDto ev = null;
            var eventService = EventFactory.MakeService(DateTime.Now);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, 3.0m);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenPriceIsLessThanZero_ThrowExceprion()
        {
            // Arrange
            var price = -10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = "Name 5",
                Description = "description event 3",
                StartDateTime = dateTime.AddDays(4),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(DateTime.Now);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<NegativePriceException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void CreateAsync_WhenNameIsNullOrWhiteSpace_ThrowException(string name)
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = name,
                Description = "description event 3",
                StartDateTime = dateTime.AddDays(4),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void CreateAsync_WhenDescriptionIsNullOrWhiteSpace_ThrowException(string description)
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = "name 0",
                Description = description,
                StartDateTime = dateTime.AddDays(4),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenStartDateTimeBeforeNow_ThrowException()
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(-5),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<UnsuitableDateTimeException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenStartDateTimeAfterFinisfDateTime_ThrowException()
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(5),
                FinishDateTime = dateTime.AddDays(3),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<UnsuitableDateTimeException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenLayoutIdDoesNotExist_ThrowException()
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 8,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(3),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenDescriptionIsNotUnique_ThrowException()
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 1,
                Name = "name 0",
                Description = "description event 2",
                StartDateTime = dateTime.AddDays(3),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenEventDoesNotHaveAnySeats_ThrowException()
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 2,
                LayoutId = 3,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(3),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<EventHasNotSeatException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenPlaceIsNotFree_ThrowException()
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 2,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(17),
                FinishDateTime = dateTime.AddDays(19),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.CreateAsync(ev, price);

            // Assert
            Assert.ThrowsAsync<UnsuitableDateTimeException>(testAction);
        }

        [Test]
        public async Task CreateAsync_WhenAllIsOK_ThenReturnCreatedEvent()
        {
            // Arrange
            var price = 10.2m;
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 2,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(2),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            var created = await eventService.CreateAsync(ev, price);

            // Assert
            created.Should().BeEquivalentTo(new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(2),
                FinishDateTime = dateTime.AddDays(5),
            });
        }

        [Test]
        public void DeleteAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var id = 0;
            var dateTime = DateTime.Now;
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void DeleteAsync_WhenIdDoesNotExist_ThrowException()
        {
            // Arrange
            var id = 12;
            var dateTime = DateTime.Now;
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetAllAsync_WhenAllIsOK_ReturnAllEventsInDtoList()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            var result = await eventService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(new List<EventDto>
            {
                    new EventDto
                    {
                        Id = 1,
                        LayoutId = 1,
                        Name = "name 1",
                        Description = "description event 1",
                        StartDateTime = dateTime.AddDays(13),
                        FinishDateTime = dateTime.AddDays(15),
                    },
                    new EventDto
                    {
                        Id = 2,
                        LayoutId = 1,
                        Name = "name 2",
                        Description = "description event 2",
                        StartDateTime = dateTime.AddDays(18),
                        FinishDateTime = dateTime.AddDays(18).AddHours(3),
                    },
                    new EventDto
                    {
                        Id = 3,
                        LayoutId = 2,
                        Name = "name 3",
                        Description = "description event 3",
                        StartDateTime = dateTime.AddDays(21),
                        FinishDateTime = dateTime.AddDays(22),
                    },
            });
        }

        [Test]
        public void GetByIdAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var id = 0;
            var dateTime = DateTime.Now;
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void GetByIdAsync_WhenIdDoesNotExist_ThrowException()
        {
            // Arrange
            var id = 12;
            var dateTime = DateTime.Now;
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetByIdAsync_WhenAllIsOK_ReturnAllEventsInDtoList()
        {
            // Arrange
            var id = 2;
            var dateTime = DateTime.Now;
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            var result = await eventService.GetByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(new EventDto
            {
                Id = 2,
                LayoutId = 1,
                Name = "name 2",
                Description = "description event 2",
                StartDateTime = new DateTime(2020, 08, 21, 21, 21, 30),
                FinishDateTime = new DateTime(2020, 09, 21, 21, 21, 30),
            });
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void UpdateAsync_WhenNameIsNullOrWhiteSpace_ThrowException(string name)
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = name,
                Description = "description event 3",
                StartDateTime = dateTime.AddDays(4),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void UpdateAsync_WhenDescriptionIsNullOrWhiteSpace_ThrowException(string description)
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = "name 0",
                Description = description,
                StartDateTime = dateTime.AddDays(4),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenStartDateTimeBeforeNow_ThrowException()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(-5),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<UnsuitableDateTimeException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenStartDateTimeAfterFinisfDateTime_ThrowException()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 3,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(5),
                FinishDateTime = dateTime.AddDays(3),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<UnsuitableDateTimeException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 0,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(3),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdDoesNotExist_ThrowException()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 13,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(3),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenDescriptionIsNotUnique_ThrowException()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 1,
                LayoutId = 1,
                Name = "name 0",
                Description = "description event 2",
                StartDateTime = dateTime.AddDays(3),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenPlaceIsNotFree_ThrowException()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 2,
                LayoutId = 2,
                Name = "name 0",
                Description = "description 0",
                StartDateTime = dateTime.AddDays(17),
                FinishDateTime = dateTime.AddDays(19),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<UnsuitableDateTimeException>(testAction);
        }

        [Test]
        public async Task UpdateAsync_WhenDescriptionIsUniqueBecauseIdIsTheSameAndAllIsOk_ThenReturnUpdatedEvent()
        {
            // Arrange
            var dateTime = DateTime.Now;
            var ev = new EventDto
            {
                Id = 2,
                LayoutId = 4,
                Name = "name 0",
                Description = "description event 2",
                StartDateTime = dateTime.AddDays(2),
                FinishDateTime = dateTime.AddDays(5),
            };
            var eventService = EventFactory.MakeService(dateTime);

            // Act
            var updated = await eventService.UpdateAsync(ev);

            // Assert
            updated.Should().BeEquivalentTo(new EventDto
            {
                Id = 2,
                LayoutId = 2,
                Name = "name 0",
                Description = "description event 2",
                StartDateTime = dateTime.AddDays(2),
                FinishDateTime = dateTime.AddDays(5),
            });
        }

        [Test]
        public void UpdateAsync_WhenUpdatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            EventDto ev = null;
            var eventService = EventFactory.MakeService(DateTime.Now);

            // Act
            AsyncTestDelegate testAction = async () => await eventService.UpdateAsync(ev);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }
    }
}