using System;
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
    internal class EventServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenInvoked_ReturneEventDtos()
        {
            // Arrange
            var eventService = EventFactory.MakeService();

            // Act
            var events = await eventService.GetAllAsync();

            // Assert
            events.Should().BeEquivalentTo(new List<EventDto>
            {
                new EventDto
                {
                    Id = 1,
                    LayoutId = 1,
                    Name = "Event 1 of layout 1",
                    Description = "event 1",
                    StartDateTime = new DateTime(2020, 11, 15, 15, 00, 00),
                    FinishDateTime = new DateTime(2020, 11, 15, 22, 45, 00),
                },
            });
        }

        [Test]
        public async Task GetByIdAsync_WhenExists_ReturnEventDto()
        {
            // Arrange
            var eventService = EventFactory.MakeService();
            var id = 1;

            // Act
            var ev = await eventService.GetByIdAsync(id);

            // Assert
            ev.Should().BeEquivalentTo(new EventDto
            {
                Id = 1,
                LayoutId = 1,
                Name = "Event 1 of layout 1",
                Description = "event 1",
                StartDateTime = new DateTime(2020, 11, 15, 15, 00, 00),
                FinishDateTime = new DateTime(2020, 11, 15, 22, 45, 00),
            });
        }

        [Test]
        public async Task CreateAsync_WhenInvoked_ReturnEventDto()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                // the difference between the returned time value from the database and the expected time differs by milliseconds
                // (if we use clear DateTime.Now)
                var dateTimeNow = DateTime.Now;
                var dateTime = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, dateTimeNow.Hour, dateTimeNow.Minute, dateTimeNow.Second);
                var eventService = EventFactory.MakeService();
                var eventDto = new EventDto
                {
                    Id = 1,
                    LayoutId = 1,
                    Name = "name 0",
                    Description = "event 0",
                    StartDateTime = dateTime.AddDays(2),
                    FinishDateTime = dateTime.AddDays(2).AddHours(5),
                };
                var price = 2.5m;

                // Act
                var ev = await eventService.CreateAsync(eventDto, price);

                // Assert
                ev.Should().BeEquivalentTo(new EventDto
                {
                    Id = ev.Id,
                    LayoutId = 1,
                    Name = "name 0",
                    Description = "event 0",
                    StartDateTime = dateTime.AddDays(2),
                    FinishDateTime = dateTime.AddDays(2).AddHours(5),
                });
            }
        }

        [Test]
        public async Task CreateAsync_WhenEventCreated_ThenEventCreatedInDB()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var eventService = EventFactory.MakeService();
                var dateTimeNow = DateTime.Now;
                var price = 2.5m;
                var eventDto = new EventDto
                {
                    Id = 1,
                    LayoutId = 1,
                    Name = "name 0",
                    Description = "event 0",
                    StartDateTime = dateTimeNow.AddDays(2),
                    FinishDateTime = dateTimeNow.AddDays(2).AddHours(5),
                };
                var listBefore = (await eventService.GetAllAsync()).ToList();

                // Act
                var ev = await eventService.CreateAsync(eventDto, price);
                listBefore.Add(ev);
                var listAfter = await eventService.GetAllAsync();

                // Assert
                listAfter.Should().BeEquivalentTo(listBefore);
            }
        }

        [Test]
        public async Task CreateAsync_WhenEventCreated_ThenEventAreaMustBeCreated()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var eventService = EventFactory.MakeService();
                var eventAreaService = EventAreaFactory.MakeService();

                var dateTimeNow = DateTime.Now;
                var price = 2.5m;
                var eventDto = new EventDto
                {
                    Id = 1,
                    LayoutId = 1,
                    Name = "name 0",
                    Description = "event 0",
                    StartDateTime = dateTimeNow.AddDays(2),
                    FinishDateTime = dateTimeNow.AddDays(2).AddHours(5),
                };

                // Act
                var ev = await eventService.CreateAsync(eventDto, price);
                var listAfter = await eventAreaService.GetAllAsync();
                var idList = listAfter.Select(eventArea => eventArea.EventId);

                // Assert
                idList.Should().Contain(ev.Id);
            }
        }

        [Test]
        public async Task CreateAsync_WhenEventCreated_ThenEventSeatMustBeCreated()
        {
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var eventService = EventFactory.MakeService();
                var eventAreaService = EventAreaFactory.MakeService();
                var eventSeatService = EventSeatFactory.MakeService();

                var dateTimeNow = DateTime.Now;
                var price = 2.5m;
                var eventDto = new EventDto
                {
                    Id = 1,
                    LayoutId = 1,
                    Name = "name 0",
                    Description = "event 0",
                    StartDateTime = dateTimeNow.AddDays(2),
                    FinishDateTime = dateTimeNow.AddDays(2).AddHours(5),
                };

                // Act
                var returnedEvent = await eventService.CreateAsync(eventDto, price);

                var eventAreaList = await eventAreaService.GetAllAsync();
                var eventSeatList = await eventSeatService.GetAllAsync();
                var hasAnySeatCreated = (from area in eventAreaList
                                         join seat in eventSeatList on area.Id equals seat.EventAreaId
                                         where area.EventId == returnedEvent.Id
                                         select seat)
                                        .Any();

                // Assert
                hasAnySeatCreated.Should().BeTrue();
            }
        }

        [Test]
        public async Task DeleteAsync_WhenEventDeleted_ThenDeletedEventNotExistInDB()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var eventService = EventFactory.MakeService();
                var id = 1;

                // Act
                await eventService.DeleteAsync(id);
                AsyncTestDelegate action = async () => await eventService.GetByIdAsync(id);

                // Assert
                Assert.ThrowsAsync<ItemExistenceException>(action);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenWeDeleteEvent_AllConnectedEventAreasShouldBeDeleted()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var eventService = EventFactory.MakeService();
                var eventAreaService = EventAreaFactory.MakeService();

                var id = 1;

                // Act
                await eventService.DeleteAsync(id);
                var list = await eventAreaService.GetAllAsync();
                var idList = list.Select(area => area.EventId);

                // Assert
                idList.Should().NotContain(id);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenWeDeleteEvent_AllConnectedEventSeatsShouldBeDeleted()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var eventService = EventFactory.MakeService();
                var eventAreaService = EventAreaFactory.MakeService();
                var eventSeatService = EventSeatFactory.MakeService();

                var id = 1;

                // Act
                await eventService.DeleteAsync(id);

                var areaList = await eventAreaService.GetAllAsync();
                var seatList = await eventSeatService.GetAllAsync();
                var isAnySeatExist = (from area in areaList
                                      join seat in seatList on area.Id equals seat.EventAreaId
                                      where area.EventId == id
                                      select seat)
                                     .Any();

                // Assert
                isAnySeatExist.Should().BeFalse();
            }
        }

        [Test]
        public async Task UpdateAsync_WhenLayoutIdIsNew_ReturnUpdatedLayoutDtoWithOldLayoutId()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // the difference between the returned time value from the database and the expected time differs by milliseconds
                // (if we use clear DateTime.Now)
                var dateTimeNow = DateTime.Now;
                var dateTime = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, dateTimeNow.Hour, dateTimeNow.Minute, dateTimeNow.Second);
                var eventService = EventFactory.MakeService();
                var eventDto = new EventDto
                {
                    Id = 1,
                    LayoutId = 2,
                    Name = "name 0",
                    Description = "event 0",
                    StartDateTime = dateTime.AddDays(2),
                    FinishDateTime = dateTime.AddDays(2).AddHours(5),
                };

                // Act
                var ev = await eventService.UpdateAsync(eventDto);

                // Assert
                ev.Should().BeEquivalentTo(new EventDto
                {
                    Id = 1,
                    LayoutId = 1,
                    Name = "name 0",
                    Description = "event 0",
                    StartDateTime = dateTime.AddDays(2),
                    FinishDateTime = dateTime.AddDays(2).AddHours(5),
                });
            }
        }
    }
}