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
    internal class VenueServiceTest
    {
        [Test]
        public async Task GetAllAsync_WhenInvoked_ReturnVenueDtos()
        {
            // Arrange
            var venueService = VenueFactory.MakeService();

            // Act
            var venues = await venueService.GetAllAsync();

            // Assert
            venues.Should().BeEquivalentTo(new List<VenueDto>
            {
                new VenueDto { Id = 1, Description = "First venue", Address = "venue address 1", Phone = "123456789001" },
            });
        }

        [Test]
        public async Task GetAllAsync_WhenPhoneIsNull_DBHasEquivalentVenueDtoList()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var venueService = VenueFactory.MakeService();
                var venueDto = new VenueDto { Id = 1, Description = "description 0", Address = "address 0", Phone = null };
                var listBefore = (await venueService.GetAllAsync()).ToList();

                // Act
                var venue = await venueService.CreateAsync(venueDto);
                listBefore.Add(venue);
                var listAfter = await venueService.GetAllAsync();

                // Assert
                listAfter.Should().BeEquivalentTo(listBefore);
            }
        }

        [Test]
        public async Task GetByIdAsync_WhenExists_ReturnVenueDto()
        {
            // Arrange
            var venueService = VenueFactory.MakeService();
            var id = 1;

            // Act
            var venues = await venueService.GetByIdAsync(id);

            // Assert
            venues.Should().BeEquivalentTo(new VenueDto { Id = 1, Description = "First venue", Address = "venue address 1", Phone = "123456789001" });
        }

        [Test]
        public async Task GetByIdAsync_WhenPhoneIsNull_ReturnEquivalentVenueDto()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var venueService = VenueFactory.MakeService();
                var venueDto = new VenueDto { Id = 1, Description = "description 0", Address = "address 0", Phone = null };

                // Act
                var inserted = await venueService.CreateAsync(venueDto);
                var returned = await venueService.GetByIdAsync(inserted.Id);
                var expected = new VenueDto { Id = inserted.Id, Description = "description 0", Address = "address 0", Phone = null };

                // Assert
                returned.Should().BeEquivalentTo(expected);
            }
        }

        [Test]
        public async Task CreateAsync_WhenInvoked_ReturnVenueDto()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var venueService = VenueFactory.MakeService();
                var venueDto = new VenueDto { Id = 1, Description = "description 0", Address= "address 0", Phone = "123456789000" };

                // Act
                var venue = await venueService.CreateAsync(venueDto);

                // Assert
                venue.Should().BeEquivalentTo(new VenueDto { Id = venue.Id, Description = "description 0", Address = "address 0", Phone = "123456789000" });
            }
        }

        [Test]
        public async Task CreateAsync_WhenVenueCreated_ThenCreatedVenueExistsInDB()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var venueService = VenueFactory.MakeService();
                var venueDto = new VenueDto { Id = 1, Description = "description 0", Address = "address 0", Phone = "123456789000" };
                var listBefore = (await venueService.GetAllAsync()).ToList();

                // Act
                var venue = await venueService.CreateAsync(venueDto);
                var listAfter = await venueService.GetAllAsync();
                listBefore.Add(venue);

                // Assert
                listAfter.Should().BeEquivalentTo(listBefore);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenVenueDeletes_ThenDeletedVenueNotExistInDB()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var venueService = VenueFactory.MakeService();
                var id = 1;

                // Act
                await venueService.DeleteAsync(id);
                AsyncTestDelegate action = async () => await venueService.GetByIdAsync(id);

                // Assert
                Assert.ThrowsAsync<ItemExistenceException>(action);
            }
        }

        [Test]
        public async Task DeleteAsync_WhenVenueDeleted_AllConnectedLayoutsShouldBeDeleted()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var venueService = VenueFactory.MakeService();
                var layoutService = LayoutFactory.MakeService();

                var id = 1;

                // Act
                await venueService.DeleteAsync(id);
                var list = await layoutService.GetAllAsync();
                var idList = list.Select(layout => layout.VenueId);

                // Assert
                idList.Should().NotContain(id);
            }
        }

        [Test]
        public async Task UpdateAsync_WhenInvoked_ReturnUpdatedVenueDto()
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Arrange
                var venueService = VenueFactory.MakeService();
                var venueDto = new VenueDto { Id = 1, Description = "description 0", Address = "address 0", Phone = "123456789000" };

                // Act
                var returned = await venueService.UpdateAsync(venueDto);

                // Assert
                returned.Should().BeEquivalentTo(await venueService.GetByIdAsync(returned.Id));
            }
        }
    }
}