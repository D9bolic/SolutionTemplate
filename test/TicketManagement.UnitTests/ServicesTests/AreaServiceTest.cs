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
    internal class AreaServiceTest
    {
        [Test]
        public void CreateAsync_WhenCreatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            AreaDto areaDto = null;
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.CreateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenLayoutIdDoesNotExist_ThrowExceprion()
        {
            // Arrange
            var areaDto = new AreaDto { Id = 1, LayoutId = 4, Description = "some description", CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.CreateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [TestCase("  ")]
        [TestCase("")]
        [TestCase(null)]
        public void CreateAsync_WhenDescriptionIsNullOrWhiteSpace_ThrowExceprion(string description)
        {
            // Arrange
            var areaDto = new AreaDto { Id = 1, LayoutId = 2, Description = description, CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.CreateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void CreateAsync_WhenDescriptionIsNotUniqueForLayout_ThrowExceprion()
        {
            // Arrange
            var areaDto = new AreaDto { Id = 1, LayoutId = 1, Description = "some description 2", CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.CreateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(testAction);
        }

        [Test]
        public async Task CreateAsync_WhenAllIsOk_ThenReturnedCreatedArea()
        {
            // Arrange
            var areaDto = new AreaDto { Id = 1, LayoutId = 1, Description = "some description 10", CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            var created = await areaService.CreateAsync(areaDto);

            // Assert
            created.Should().BeEquivalentTo(new AreaDto { Id = 1, LayoutId = 1, Description = "some description 10", CoordX = -1, CoordY = -3 });
        }

        [Test]
        public void DeleteAsync_WhenIdIsNotMoreThanZero_ThrowExceprion()
        {
            // Arrange
            var id = 0;
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void DeleteAsync_WhenIdDoesNotExist_ThrowExceprion()
        {
            // Arrange
            var id = 11;
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.DeleteAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetAllAsync_WhenAllIsOK_ReturnAllAreasInDtoList()
        {
            // Arrange
            var areaService = AreaFactory.MakeService();

            // Act
            var result = await areaService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(new List<AreaDto>
            {
                new AreaDto { Id = 1, LayoutId = 1, Description = "some description 1", CoordX = 1, CoordY = -1 },
                new AreaDto { Id = 2, LayoutId = 1, Description = "some description 2", CoordX = -1, CoordY = 1 },
                new AreaDto { Id = 3, LayoutId = 2, Description = "some description 3", CoordX = 1, CoordY = 1 },
            });
        }

        [Test]
        public void GetByIdAsync_WhenIdIsNotMoreThanOne_ThrowExceprion()
        {
            // Arrange
            var id = 0;
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void GetByIdAsync_WhenIdDoesNotExist_ThrowExceprion()
        {
            // Arrange
            var id = 11;
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.GetByIdAsync(id);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public async Task GetByIdAsync_WhenAllIsOK_ReturnAreaDto()
        {
            // Arrange
            var id = 3;
            var areaService = AreaFactory.MakeService();

            // Act
            var area = await areaService.GetByIdAsync(id);

            // Assert
            area.Should().BeEquivalentTo(new AreaDto { Id = 3, LayoutId = 2, Description = "some description 3", CoordX = 1, CoordY = 1 });
        }

        [TestCase("  ")]
        [TestCase("")]
        [TestCase(null)]
        public void UpdateAsync_WhenDescriptionIsNullOrWhiteSpace_ThrowExceprion(string description)
        {
            // Arrange
            var areaDto = new AreaDto { Id = 1, LayoutId = 2, Description = description, CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.UpdateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<ModelValidationException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdIsNotMoreThanOne_ThrowExceprion()
        {
            // Arrange
            var areaDto = new AreaDto { Id = 0, LayoutId = 2, Description = "some description 4", CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.UpdateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<NotPossitiveIdException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenIdDoesNotExist_ThrowExceprion()
        {
            // Arrange
            var areaDto = new AreaDto { Id = 11, LayoutId = 2, Description = "some description 4", CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.UpdateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenLayoutIdDoesNotExist_ThrowExceprion()
        {
            // Arrange
            var areaDto = new AreaDto { Id = 1, LayoutId = 4, Description = "some description", CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.UpdateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<ItemExistenceException>(testAction);
        }

        [Test]
        public void UpdateAsync_WhenDescriptionIsNotUniqueForLayout_ThrowExceprion()
        {
            // Arrange
            var areaDto = new AreaDto { Id = 1, LayoutId = 1, Description = "some description 2", CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.UpdateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<DescriptionUniqueException>(testAction);
        }

        [Test]
        public async Task UpdateAsync_WhenDescriptionUniqueForLayoutBecauseIdIsTheSameAndAllIsOk_ThenReturnUpdatedArea()
        {
            // Arrange
            var areaDto = new AreaDto { Id = 2, LayoutId = 1, Description = "some description 2", CoordX = -1, CoordY = -3 };
            var areaService = AreaFactory.MakeService();

            // Act
            var updated = await areaService.UpdateAsync(areaDto);

            // Assert
            updated.Should().BeEquivalentTo(new AreaDto { Id = 2, LayoutId = 1, Description = "some description 2", CoordX = -1, CoordY = -3 });
        }

        [Test]
        public void UpdateAsync_WhenUpdatableItemIsNull_ThrowExceprion()
        {
            // Arrange
            AreaDto areaDto = null;
            var areaService = AreaFactory.MakeService();

            // Act
            AsyncTestDelegate testAction = async () => await areaService.UpdateAsync(areaDto);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(testAction);
        }
    }
}
