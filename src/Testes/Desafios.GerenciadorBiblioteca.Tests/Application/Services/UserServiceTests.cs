using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services;
using Moq;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Tests.Application.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenUsersExist_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "User 1", Email = "user1@example.com", Phone = "111111111" },
                new User { Id = 2, Name = "User 2", Email = "user2@example.com", Phone = "222222222" }
            };
            _mockUnitOfWork.Setup(uow => uow.Users.GetAllAsync())
                           .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count());
            Assert.Equal(users, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsValid_ShouldReturnUser()
        {
            // Arrange
            int userId = 1;
            var user = new User { Id = userId, Name = "User 1", Email = "user1@example.com", Phone = "111111111" };
            _mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(userId))
                           .ReturnsAsync(user);

            // Act
            var result = await _userService.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.GetByIdAsync(invalidId));
            Assert.Equal("O parâmetro Id deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task GetByNameAsync_WhenNameMatches_ShouldReturnFilteredUsers()
        {
            // Arrange
            var name = "User";
            var users = new List<User>
            {
                new User { Id = 1, Name = "User 1", Email = "user1@example.com", Phone = "111111111" },
                new User { Id = 2, Name = "Another User", Email = "user2@example.com", Phone = "222222222" }
            };
            _mockUnitOfWork.Setup(uow => uow.Users.GetAllAsync())
                           .ReturnsAsync(users);

            // Act
            var result = await _userService.GetByNameAsync(name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, user => user.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));
        }

        [Fact]
        public async Task GetByNameAsync_WhenNameDoesNotMatch_ShouldReturnEmptyList()
        {
            // Arrange
            var name = "Nonexistent";
            var users = new List<User>
            {
                new User { Id = 1, Name = "User 1", Email = "user1@example.com", Phone = "111111111" },
                new User { Id = 2, Name = "Another User", Email = "user2@example.com", Phone = "222222222" }
            };
            _mockUnitOfWork.Setup(uow => uow.Users.GetAllAsync())
                           .ReturnsAsync(users);

            // Act
            var result = await _userService.GetByNameAsync(name);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByNameAsync_WhenNameIsNull_ShouldReturnAllUsers()
        {
            // Arrange
            string name = null;
            var users = new List<User>
            {
                new User { Id = 1, Name = "User 1", Email = "user1@example.com", Phone = "111111111" },
                new User { Id = 2, Name = "Another User", Email = "user2@example.com", Phone = "222222222" }
            };
            _mockUnitOfWork.Setup(uow => uow.Users.GetAllAsync())
                           .ReturnsAsync(users);

            // Act
            var result = await _userService.GetByNameAsync(name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count());
            Assert.Equal(users, result);
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsValid_ShouldReturnAddedUser()
        {
            // Arrange
            var dto = new UserDTO("User 1", "user1@example.com", "111111111");
            var user = new User { Id = 1, Name = "User 1", Email = "user1@example.com", Phone = "111111111" };
            _mockMapper.Setup(m => m.Map<User>(dto)).Returns(user);
            _mockUnitOfWork.Setup(uow => uow.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(user);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _userService.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Phone, result.Phone);
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsNull_ShouldThrowCustomException()
        {
            // Arrange
            UserDTO dto = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.AddAsync(dto));
            Assert.Equal("O parâmetro Usuário não pode ser nulo.", exception.ErrorDetails);
        }

        [Fact]
        public async Task AddAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            var dto = new UserDTO("User 1", "user1@example.com", "111111111");
            var user = new User { Id = 1, Name = "User 1", Email = "user1@example.com", Phone = "111111111" };
            _mockMapper.Setup(m => m.Map<User>(dto)).Returns(user);
            _mockUnitOfWork.Setup(uow => uow.Users.AddAsync(It.IsAny<User>())).ReturnsAsync(user);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.AddAsync(dto));
            Assert.Equal("Não foi possível adicionar o Usuário. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsValid_ShouldReturnUpdatedUser()
        {
            // Arrange
            int userId = 1;
            var dto = new UserDTO("Updated User", "updated@example.com", "222222222");
            var existingUser = new User { Id = userId, Name = "Old User", Email = "old@example.com", Phone = "111111111" };

            _mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _userService.UpdateAsync(userId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.Phone, result.Phone);
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsNull_ShouldThrowCustomException()
        {
            // Arrange
            int userId = 1;
            UserDTO dto = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.UpdateAsync(userId, dto));
            Assert.Equal("O parâmetro Usuário não pode ser nulo.", exception.ErrorDetails);
        }

        [Fact]
        public async Task UpdateAsync_WhenUserNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int userId = 1;
            var dto = new UserDTO("Updated User", "updated@example.com", "222222222");

            _mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.UpdateAsync(userId, dto));
            Assert.Equal("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int userId = 1;
            var dto = new UserDTO("Updated User", "updated@example.com", "222222222");
            var existingUser = new User { Id = userId, Name = "Old User", Email = "old@example.com", Phone = "111111111" };

            _mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.UpdateAsync(userId, dto));
            Assert.Equal("Não foi possível alterar o Usuário. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsValid_ShouldReturnTrue()
        {
            // Arrange
            int userId = 1;
            var existingUser = new User { Id = userId, Name = "User to be deleted", Email = "user@example.com", Phone = "111111111" };

            _mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _userService.RemoveAsync(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.RemoveAsync(invalidId));
            Assert.Equal("O parâmetro Id deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task RemoveAsync_WhenUserNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int userId = 1;

            _mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.RemoveAsync(userId));
            Assert.Equal("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int userId = 1;
            var existingUser = new User { Id = userId, Name = "User to be deleted", Email = "user@example.com", Phone = "111111111" };

            _mockUnitOfWork.Setup(uow => uow.Users.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _userService.RemoveAsync(userId));
            Assert.Equal("Não foi possível deletar o Usuário. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }
    }
}
