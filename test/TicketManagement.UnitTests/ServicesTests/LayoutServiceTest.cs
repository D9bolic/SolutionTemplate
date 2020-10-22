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
    internal class LayoutServiceTest
    {
        [Test]
        public void CreateAsync_WhenCreatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            LayoutDto layoutDto = null;
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.CreateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenVenueIdDoesNotExist_ThrowExceprion()
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 1, VenueId = 4, Description = "some description" };
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.CreateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void CreateAsync_WhenDescriptionIsNullOrWhiteSpace_ThrowExceprion(string description)
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 1, VenueId = 3, Description = description };
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.CreateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenDescriptionIsNotUniqueForVenue_ThrowExceprion()
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 1, VenueId = 1, Description = "description 2" };
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.CreateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(testAction);
        }

        [Test]
        public async Task CreateAsync_WhenDescriptionIsUniqueForVenueAndAllIsOK_ThenReturnCreatedlayout()
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 1, VenueId = 3, Description = "description 4" };
            var layoutService = LayoutFactory.MakeService();

            // Act
            var created = await layoutService.CreateAsync(layoutDto);

            // Assert
            created.Should().BeEquivalentTo(new LayoutDto { Id = 4, VenueId = 3, Description = "description 4" });
        }

        [Test]
        public void DeleteAsync_WhenIdIsNotMoreThanZero_ThrowExceprion()
        {
            // Arrange
            var id = 0;
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void DeleteAsync_WhenIdIsNotExist_ThrowExceprion()
        {
            // Arrange
            var id = 5;
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetAllAsync_WhenAllIsOK_ReturnAllLayoutsInDtoList()
        {
            // Arrange
            var layoutService = LayoutFactory.MakeService();

            // Act
            var result = await layoutService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(new List<LayoutDto>
            {
                new LayoutDto { Id = 1, VenueId = 1, Description = "description 1" },
                new LayoutDto { Id = 2, VenueId = 1, Description = "description 2" },
                new LayoutDto { Id = 3, VenueId = 2, Description = "description 3" },
            });
        }

        [Test]
        public void GetByIdAsync_WhenIdIsNotMoreThanZero_ThrowExceprion()
        {
            // Arrange
            var id = -5;
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void GetByIdAsync_WhenIdNotExists_ThrowExceprion()
        {
            // Arrange
            var id = 5;
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetByIdAsync_WhenAllIsOK_ReturnLayoutDto()
        {
            // Arrange
            var id = 3;
            var layoutService = LayoutFactory.MakeService();

            // Act
            var layout = await layoutService.GetByIdAsync(id);

            // Assert
            layout.Should().BeEquivalentTo(new LayoutDto { Id = 3, VenueId = 2, Description = "some description 1" });
        }

        [Test]
        public void UpdateAsync_WhenIdIsNotMoreThanZero_ThrowExceprion()
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 0, VenueId = 2, Description = "some description 1" };
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.UpdateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdDoesNotExist_ThrowExceprion()
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 5, VenueId = 2, Description = "some description 5" };
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.UpdateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenVenueIdDoesNotExist_ThrowExceprion()
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 2, VenueId = 5, Description = "some description 5" };
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.UpdateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("   ")]
        public void UpdateAsync_WhenDescriptionIsNullOrWhiteSpaces_ThrowExceprion(string description)
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 2, VenueId = 2, Description = description };
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.UpdateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenDescriptionIsNotUniqueBecauseIdIsNotTheSameForVenue_ThrowExceprion()
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 1, VenueId = 1, Description = "description 2" };
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.UpdateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(testAction);
        }

        [Test]
        public async Task UpdateAsync_WhenDescriptionIsUniqueForVenueBecauseIdIsTheSameAndAllIsOk_ThenReturnUpdatedlayout()
        {
            // Arrange
            var layoutDto = new LayoutDto { Id = 2, VenueId = 2, Description = "description 1" };
            var layoutService = LayoutFactory.MakeService();

            // Act
            var updated = await layoutService.UpdateAsync(layoutDto);

            // Assert
            updated.Should().BeEquivalentTo(new LayoutDto { Id = 2, VenueId = 2, Description = "description 1" });
        }

        [Test]
        public void UpdateAsync_WhenUpdatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            LayoutDto layoutDto = null;
            var layoutService = LayoutFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await layoutService.UpdateAsync(layoutDto);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }
    }
}