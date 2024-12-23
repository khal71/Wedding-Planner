
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlannerApplication.Services.ServicesImplementation;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerDomain;
using Xunit;

namespace WpUnitTest
{
    public class FlowerServiceTests
        {
            private readonly Mock<IFlowerRepo> _flowerRepoMock;
            private readonly FlowerService _flowerService;

            public FlowerServiceTests()
            {
                _flowerRepoMock = new Mock<IFlowerRepo>();
                _flowerService = new FlowerService(_flowerRepoMock.Object);
            }

            // Test AddAsync method
            [Fact]
            public async Task AddAsync_ShouldReturnFlower_WhenFlowerIsValid()
            {
                // Arrange
                var newFlower = new Flower { Name = "Rose", Type = "Red", Color = "Red" };
                _flowerRepoMock.Setup(repo => repo.AddAsync(It.IsAny<Flower>())).ReturnsAsync(newFlower);

                // Act
                var result = await _flowerService.AddAsync(newFlower);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(newFlower, result.Model);
            }

            [Fact]
            public async Task AddAsync_ShouldReturnError_WhenFlowerIsNull()
            {
                // Act
                var result = await _flowerService.AddAsync(null);

                // Assert
                Assert.Equal("Flower was mot created", result.Message);
            }

            // Test DeleteAsync method
            [Fact]
            public async Task DeleteAsync_ShouldReturnFlower_WhenFlowerExists()
            {
                // Arrange
                var flowerId = 1;
                var flower = new Flower { Id = flowerId, Name = "Rose", Type = "Red", Color = "Red" };
                _flowerRepoMock.Setup(repo => repo.DeleteAsync(flowerId)).ReturnsAsync(flower);

                // Act
                var result = await _flowerService.DeleteAsync(flowerId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(flower, result.Model);
            }

            [Fact]
            public async Task DeleteAsync_ShouldReturnError_WhenFlowerNotFound()
            {
                // Arrange
                var flowerId = 999;  // Non-existing ID
                _flowerRepoMock.Setup(repo => repo.DeleteAsync(flowerId)).ReturnsAsync((Flower)null);

                // Act
                var result = await _flowerService.DeleteAsync(flowerId);

                // Assert
                Assert.Equal("Flower was not deleted", result.Message);
            }

            // Test GetByIdAsync method
            [Fact]
            public async Task GetByIdAsync_ShouldReturnFlower_WhenFlowerExists()
            {
                // Arrange
                var flowerId = 1;
                var flower = new Flower { Id = flowerId, Name = "Rose", Type = "Red", Color = "Red" };
                _flowerRepoMock.Setup(repo => repo.GetByIdAsync(flowerId)).ReturnsAsync(flower);

                // Act
                var result = await _flowerService.GetByIdAsync(flowerId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(flower, result.Model);
            }

         

            // Test ListAsync method
            [Fact]
            public async Task ListAsync_ShouldReturnListOfFlowers_WhenFlowersExist()
            {
                // Arrange
                var flowers = new List<Flower>
            {
                new Flower { Id = 1, Name = "Rose", Type = "Red", Color = "Red" },
                new Flower { Id = 2, Name = "Tulip", Type = "Yellow", Color = "Yellow" }
            };
                _flowerRepoMock.Setup(repo => repo.ListAsync()).ReturnsAsync(flowers);

                // Act
                var result = await _flowerService.ListAsync();

                // Assert
                Assert.NotEmpty(result);
                Assert.Equal(2, result.Count);
            }

            [Fact]
            public async Task ListAsync_ShouldReturnEmptyList_WhenNoFlowersExist()
            {
                // Arrange
                var flowers = new List<Flower>();
                _flowerRepoMock.Setup(repo => repo.ListAsync()).ReturnsAsync(flowers);

                // Act
                var result = await _flowerService.ListAsync();

                // Assert
                Assert.Empty(result);
            }

            // Test UpdateAsync method
            [Fact]
            public async Task UpdateAsync_ShouldReturnUpdatedFlower_WhenFlowerIsValid()
            {
                // Arrange
                var flowerId = 1;
                var updatedFlower = new Flower { Id = flowerId, Name = "Updated Rose", Type = "Pink", Color = "Pink" };
                var existingFlower = new Flower { Id = flowerId, Name = "Rose", Type = "Red", Color = "Red" };

                _flowerRepoMock.Setup(repo => repo.GetByIdAsync(flowerId)).ReturnsAsync(existingFlower);
                _flowerRepoMock.Setup(repo => repo.UpdateAsync(flowerId, updatedFlower)).ReturnsAsync(updatedFlower);

                // Act
                var result = await _flowerService.UpdateAsync(flowerId, updatedFlower);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(updatedFlower.Name, result.Model.Name);
                Assert.Equal(updatedFlower.Type, result.Model.Type);
            }

            [Fact]
            public async Task UpdateAsync_ShouldReturnError_WhenFlowerNotFound()
            {
                // Arrange
                var flowerId = 999;  // Non-existing ID
                var updatedFlower = new Flower { Id = flowerId, Name = "Updated Rose", Type = "Pink", Color = "Pink" };

                _flowerRepoMock.Setup(repo => repo.GetByIdAsync(flowerId)).ReturnsAsync((Flower)null);

                // Act
                var result = await _flowerService.UpdateAsync(flowerId, updatedFlower);

                // Assert
                Assert.Equal("Flower was not updated", result.Message);
            }
        }
    }
