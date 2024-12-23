using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeddingPlannerApplication.Services.ServicesImplementation;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerDomain.Entities;
using WeddingPlannerDomain;
using Xunit;
using WeddingPlanner.Helpers;

namespace WeddingPlanner.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepo> _userRepoMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepo>();
            _userService = new UserService(_userRepoMock.Object);
        }

        // Test AddAsync method
        [Fact]
        public async Task AddAsync_ShouldReturnUser_WhenUserIsValid()
        {
            // Arrange
            var newUser = new User { Id = 1, Email = "test@example.com", Password = "password" };
            _userRepoMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(newUser);

            // Act
            var result = await _userService.AddAsync(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser, result.Model);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnError_WhenUserIsNull()
        {
            // Act
            var result = await _userService.AddAsync(null);

            // Assert
            Assert.Equal("User was mot created", result.Message);
        }

        // Test DeleteAsync method
        [Fact]
        public async Task DeleteAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Email = "test@example.com", Password = "password" };
            _userRepoMock.Setup(repo => repo.DeleteAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.DeleteAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            var userId = 999;  // Non-existing user
            _userRepoMock.Setup(repo => repo.DeleteAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.DeleteAsync(userId);

            // Assert
            Assert.Equal("User was not deleted", result.Message);
        }

        // Test GetByIdAsync method
        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Email = "test@example.com", Password = "password" };
            _userRepoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

     

        // Test ListAsync method
        [Fact]
        public async Task ListAsync_ShouldReturnListOfUsers_WhenUsersExist()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Email = "test@example.com", Password = "password" },
                new User { Id = 2, Email = "test2@example.com", Password = "password2" }
            };
            _userRepoMock.Setup(repo => repo.ListAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.ListAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<User>();
            _userRepoMock.Setup(repo => repo.ListAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.ListAsync();

            // Assert
            Assert.Empty(result);
        }

        // Test UpdateAsync method
        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedUser_WhenUserIsValid()
        {
            // Arrange
            var userId = 1;
            var updatedUser = new User { Id = userId, Email = "updated@example.com", Password = "newpassword" };
            var existingUser = new User { Id = userId, Email = "test@example.com", Password = "password" };

            _userRepoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _userRepoMock.Setup(repo => repo.UpdateAsync(userId, updatedUser)).ReturnsAsync(updatedUser);

            // Act
            var result = await _userService.UpdateAsync(userId, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Email, result.Model.Email);
            Assert.Equal(updatedUser.Password, result.Model.Password);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            var userId = 999;  // Non-existing user
            var updatedUser = new User { Id = userId, Email = "updated@example.com", Password = "newpassword" };

            _userRepoMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.UpdateAsync(userId, updatedUser);

            // Assert
            Assert.Equal("User was not updated", result.Message);
        }

        // Test FindByEmailAsync method
        [Fact]
        public async Task FindByEmailAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { Id = 1, Email = email, Password = "password" };
            _userRepoMock.Setup(repo => repo.FindByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var result = await _userService.FindByEmailAsync(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public async Task FindByEmailAsync_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";
            _userRepoMock.Setup(repo => repo.FindByEmailAsync(email)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.FindByEmailAsync(email);

            // Assert
            Assert.Equal("User was not found", result.Message);
        }

      

        [Fact]
        public void ValidatePasswordAsync_ShouldReturnFalse_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var hasher = new PasswhordHash();
            var password = "password";
            var wrongPassword = "wrongpassword";
            var hashedPassword = hasher.HashPassword(password);

            // Act
            var result = _userService.ValidatePasswordAsync(wrongPassword, hashedPassword);

            // Assert
            Assert.False(result);
        }
    }
}

