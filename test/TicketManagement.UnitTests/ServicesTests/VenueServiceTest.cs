using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ticketmanagement.BusinessLogic.CustomExceptions;
using Ticketmanagement.BusinessLogic.DTO;
using Ticketmanagement.BusinessLogic.Services;
using TicketManagement.DataAccess.Repositories.Interfaces;
using TicketManagement.UnitTests.Factories;

namespace TicketManagement.UnitTests.ServicesTests
{
    [TestFixture]
    internal class VenueServiceTest
    {
        [Test]
        public void CreateAsync_WhenCreatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            VenueDto venue = null;
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.CreateAsync(venue);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void CreateAsync_WhenAddressIsNullOrWhiteSpace_ThrowException(string address)
        {
            // Arrange
            var venueDto = new VenueDto { Id = 1, Address = address, Description = "some description", Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.CreateAsync(venueDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void CreateAsync_WhenDescriptionIsNullOrWhiteSpace_ThrowException(string description)
        {
            // Arrange
            var venueDto = new VenueDto { Id = 1, Address = "some address", Description = description, Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.CreateAsync(venueDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenDescriptionIsNotUnique_ThrowException()
        {
            // Arrange
            var venueDto = new VenueDto { Id = 2, Address = "some address 4", Description = "some description 2", Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.CreateAsync(venueDto);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(testAction);
        }

        [TestCase("1234567890123")]
        [TestCase("12345678901")]
        [TestCase("12345678901q")]
        [TestCase("w23456789012")]
        [TestCase("223456f89012")]
        [TestCase("12345678901Q")]
        [TestCase("W23456789012")]
        [TestCase("223456F89012")]
        [TestCase("223456389,12")]
        [TestCase("2234 6389212")]
        [TestCase("22346389.212")]
        [TestCase("223463893212a")]
        [TestCase("v234638932122")]
        [TestCase("223463893212A")]
        [TestCase("V234638932122")]
        public void CreateAsync_WhenPhoneIsBad_ThrowException(string phone)
        {
            // Arrange
            var venueDto = new VenueDto { Id = 5, Address = "some address", Description = "unique 0", Phone = phone };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.CreateAsync(venueDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("123456789012")]
        public void CreateAsync_WhenPhoneIsGood_NotThrowException(string phone)
        {
            // Arrange
            var venueDto = new VenueDto { Id = 2, Address = "some address", Description = "unique 0", Phone = phone };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.CreateAsync(venueDto);

            // Assert
            Assert.DoesNotThrowAsync(testAction);
        }

        [Test]
        public async Task CreateAsync_WhenAllIsOk_ReturnCreatedVenue()
        {
            // Arrange
            var venueDto = new VenueDto { Id = 2,  Address = "some address 4", Description = "unique 3", Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            var created = await venueService.CreateAsync(venueDto);

            // Assert
            created.Should().BeEquivalentTo(new VenueDto { Id = 4, Address = "some address 4", Description = "unique 3", Phone = "123456789012" });
        }

        [Test]
        public void DeleteAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var id = -2;
            var venueRepository = new Mock<IVenueRepository>();
            var venueService = new VenueService(venueRepository.Object);

            // Act
            AsyncTestDelegate testAction = async () => await venueService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void DeleteAsync_WhenIdNotExists_ThrowException()
        {
            // Arrange
            var id = 4;
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetAllAsync_WhenAllIsOK_ReturnAllVenuesInDtoList()
        {
            // Arrange
            var venueService = VenueFactory.MakeService();

            // Act
            var result = await venueService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(new List<VenueDto>
            {
                new VenueDto { Id = 1, Address = "some address 1", Description = "some description 1", Phone = "123456789011" },
                new VenueDto { Id = 2, Address = "some address 2", Description = "some description 2", Phone = "123456789012" },
                new VenueDto { Id = 3, Address = "some address 3", Description = "some description 3", Phone = "123456789013" },
            });
        }

        [Test]
        public void GetByIdAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var id = 0;
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void GetByIdAsync_WhenIdNotExists_ThrowException()
        {
            // Arrange
            var id = 5;
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetByIdAsync_WhenAllIsOK_ReturnVenueDto()
        {
            // Arrange
            var id = 1;
            var venueService = VenueFactory.MakeService();

            // Act
            var result = await venueService.GetByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(new VenueDto { Id = 1, Address = "addres 1", Description = "unique 1", Phone = "123456789012" });
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void UpdateAsync_WhenAddresIsNullOrWhiteSpace_ThrowException(string address)
        {
            // Arrange
            var venue = new VenueDto { Id = 1, Address = address,  Description = "some description", Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.UpdateAsync(venue);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void UpdateAsync_WhenDescriptionIsNullOrWhiteSpace_ThrowException(string description)
        {
            // Arrange
            var venue = new VenueDto { Id = 1, Address = "some address", Description = description, Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.UpdateAsync(venue);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdIsNotMoreThanZero_ThrowException()
        {
            // Arrange
            var venue = new VenueDto { Id = 0, Address = "some address", Description = "some description", Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.UpdateAsync(venue);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdNotExists_ThrowException()
        {
            // Arrange
            var venue = new VenueDto { Id = 5, Address = "some address", Description = "some description", Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.UpdateAsync(venue);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenDescriptionIsNotUnique_ThrowException()
        {
            // Arrange
            var venue = new VenueDto { Id = 1, Address = "some address", Description = "some description 2", Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate result = async () => await venueService.UpdateAsync(venue);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(result);
        }

        [TestCase("1234567890123")]
        [TestCase("12345678901")]
        [TestCase("12345678901q")]
        [TestCase("w23456789012")]
        [TestCase("223456f89012")]
        [TestCase("12345678901Q")]
        [TestCase("W23456789012")]
        [TestCase("223456F89012")]
        [TestCase("223456389,12")]
        [TestCase("2234 6389212")]
        [TestCase("22346389.212")]
        [TestCase("223463893212a")]
        [TestCase("v234638932122")]
        [TestCase("223463893212A")]
        [TestCase("V234638932122")]
        public void UpdateAsync_WhenPhoneIsBad_ThrowException(string phone)
        {
            // Arrange
            var venueDto = new VenueDto { Id = 5, Address = "some address", Description = "unique 0", Phone = phone };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.UpdateAsync(venueDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("123456789012")]
        public void UpdateAsync_WhenPhoneIsGood_NotThrowException(string phone)
        {
            // Arrange
            var venueDto = new VenueDto { Id = 2, Address = "some address", Description = "unique 0", Phone = phone };
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.UpdateAsync(venueDto);

            // Assert
            Assert.DoesNotThrowAsync(testAction);
        }

        [Test]
        public async Task UpdateAsync_WhenDescriptionIsTheSameForTheSameId_ReturnUpdatedVenue()
        {
            // Arrange
            var venue = new VenueDto { Id = 2, Address = "some address", Description = "some description 2", Phone = "123456789012" };
            var venueService = VenueFactory.MakeService();

            // Act
            var updated = await venueService.UpdateAsync(venue);

            // Assert
            updated.Should().BeEquivalentTo(new VenueDto { Id = 2, Address = "some address", Description = "some description 2", Phone = "123456789012" });
        }

        [Test]
        public void UpdateAsync_WhenUpdatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            VenueDto venue = null;
            var venueService = VenueFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await venueService.UpdateAsync(venue);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }
    }
}