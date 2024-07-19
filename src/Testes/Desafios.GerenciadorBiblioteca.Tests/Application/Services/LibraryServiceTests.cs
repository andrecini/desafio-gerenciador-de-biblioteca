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
    public class LibraryServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LibraryService _libraryService;

        public LibraryServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _libraryService = new LibraryService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenLibrariesExist_ShouldReturnAllLibraries()
        {
            // Arrange
            var libraries = new List<Library>
            {
                new Library { Id = 1, Name = "Library 1", CNPJ = "123456", Phone = "111111111" },
                new Library { Id = 2, Name = "Library 2", CNPJ = "654321", Phone = "222222222" }
            };
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetAllAsync())
                           .ReturnsAsync(libraries);

            // Act
            var result = await _libraryService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(libraries.Count, result.Count());
            Assert.Equal(libraries, result);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoLibrariesExist_ShouldReturnEmptyList()
        {
            // Arrange
            var libraries = new List<Library>();
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetAllAsync())
                           .ReturnsAsync(libraries);

            // Act
            var result = await _libraryService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_WhenExceptionOccurs_ShouldThrowException()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetAllAsync())
                           .ThrowsAsync(new System.Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _libraryService.GetAllAsync());
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsValid_ShouldReturnLibrary()
        {
            // Arrange
            int libraryId = 1;
            var library = new Library { Id = libraryId, Name = "Library 1", CNPJ = "123456", Phone = "111111111" };
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId))
                           .ReturnsAsync(library);

            // Act
            var result = await _libraryService.GetByIdAsync(libraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(libraryId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.GetByIdAsync(invalidId));
            Assert.Contains("O parâmetro Id deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task GetByIdAsync_WhenLibraryNotFound_ShouldReturnNull()
        {
            // Arrange
            int libraryId = 1;
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId))
                           .ReturnsAsync((Library)null);

            // Act
            var result = await _libraryService.GetByIdAsync(libraryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenExceptionOccurs_ShouldThrowException()
        {
            // Arrange
            int libraryId = 1;
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId))
                           .ThrowsAsync(new System.Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _libraryService.GetByIdAsync(libraryId));
        }

        [Fact]
        public async Task GetByNameAsync_WhenNameMatches_ShouldReturnFilteredLibraries()
        {
            // Arrange
            var name = "Library";
            var libraries = new List<Library>
            {
                new Library { Id = 1, Name = "Library 1", CNPJ = "123456", Phone = "111111111" },
                new Library { Id = 2, Name = "Another Library", CNPJ = "654321", Phone = "222222222" }
            };
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetAllAsync())
                           .ReturnsAsync(libraries);

            // Act
            var result = await _libraryService.GetByNameAsync(name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, lib => lib.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));
        }

        [Fact]
        public async Task GetByNameAsync_WhenNameDoesNotMatch_ShouldReturnEmptyList()
        {
            // Arrange
            var name = "Nonexistent";
            var libraries = new List<Library>
            {
                new Library { Id = 1, Name = "Library 1", CNPJ = "123456", Phone = "111111111" },
                new Library { Id = 2, Name = "Another Library", CNPJ = "654321", Phone = "222222222" }
            };
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetAllAsync())
                           .ReturnsAsync(libraries);

            // Act
            var result = await _libraryService.GetByNameAsync(name);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByNameAsync_WhenNameIsNull_ShouldReturnAllLibraries()
        {
            // Arrange
            string name = null;
            var libraries = new List<Library>
            {
                new Library { Id = 1, Name = "Library 1", CNPJ = "123456", Phone = "111111111" },
                new Library { Id = 2, Name = "Another Library", CNPJ = "654321", Phone = "222222222" }
            };
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetAllAsync())
                           .ReturnsAsync(libraries);

            // Act
            var result = await _libraryService.GetByNameAsync(name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(libraries.Count, result.Count());
            Assert.Equal(libraries, result);
        }

        [Fact]
        public async Task GetByNameAsync_WhenExceptionOccurs_ShouldThrowException()
        {
            // Arrange
            var name = "Library";
            _mockUnitOfWork.Setup(uow => uow.Libraries.GetAllAsync())
                           .ThrowsAsync(new System.Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _libraryService.GetByNameAsync(name));
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsValid_ShouldReturnAddedLibrary()
        {
            // Arrange
            var dto = new LibraryInputModel("New Library", "123456", "111111111");
            var library = new Library { Id = 1, Name = "New Library", CNPJ = "123456", Phone = "111111111" };
            _mockMapper.Setup(m => m.Map<Library>(dto)).Returns(library);
            _mockUnitOfWork.Setup(uow => uow.Libraries.AddAsync(It.IsAny<Library>())).ReturnsAsync(library);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _libraryService.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(library.Name, result.Name);
            Assert.Equal(library.CNPJ, result.CNPJ);
            Assert.Equal(library.Phone, result.Phone);
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsNull_ShouldThrowCustomException()
        {
            // Arrange
            LibraryInputModel dto = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.AddAsync(dto));
            Assert.Contains("O parâmetro Biblioteca não pode ser nulo.", exception.ErrorDetails);
        }

        [Fact]
        public async Task AddAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            var dto = new LibraryInputModel("New Library", "123456", "111111111");
            var library = new Library { Id = 1, Name = "New Library", CNPJ = "123456", Phone = "111111111" };
            _mockMapper.Setup(m => m.Map<Library>(dto)).Returns(library);
            _mockUnitOfWork.Setup(uow => uow.Libraries.AddAsync(It.IsAny<Library>())).ReturnsAsync(library);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.AddAsync(dto));
            Assert.Contains("Não foi possível adicionar a Biblioteca. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task AddAsync_WhenValidationFails_ShouldThrowValidationException()
        {
            // Arrange
            var dto = new LibraryInputModel("Invalid Library", "123bb6", "111111111");

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _libraryService.AddAsync(dto));
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsValid_ShouldReturnUpdatedLibrary()
        {
            // Arrange
            int libraryId = 1;
            var dto = new LibraryInputModel("Updated Library", "654321", "222222222");
            var existingLibrary = new Library { Id = libraryId, Name = "Old Library", CNPJ = "123456", Phone = "111111111" };

            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId)).ReturnsAsync(existingLibrary);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _libraryService.UpdateAsync(libraryId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.CNPJ, result.CNPJ);
            Assert.Equal(dto.Phone, result.Phone);
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsNull_ShouldThrowCustomException()
        {
            // Arrange
            int libraryId = 1;
            LibraryInputModel dto = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.UpdateAsync(libraryId, dto));
            Assert.Contains("O parâmetro Biblioteca não pode ser nulo.", exception.ErrorDetails);
        }

        [Fact]
        public async Task UpdateAsync_WhenLibraryNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int libraryId = 1;
            var dto = new LibraryInputModel("Updated Library", "654321", "222222222");

            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId)).ReturnsAsync((Library)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.UpdateAsync(libraryId, dto));
            Assert.Contains("Nenhuma Biblioteca foi encontrada com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int libraryId = 1;
            var dto = new LibraryInputModel("Updated Library", "654321", "222222222");
            var existingLibrary = new Library { Id = libraryId, Name = "Old Library", CNPJ = "123456", Phone = "111111111" };

            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId)).ReturnsAsync(existingLibrary);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.UpdateAsync(libraryId, dto));
            Assert.Contains("Não foi possível alterar a Biblioteca. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenValidationFails_ShouldThrowValidationException()
        {
            // Arrange
            int libraryId = 1;
            var dto = new LibraryInputModel("Invalid Library", "654bb1", "222222222");

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _libraryService.UpdateAsync(libraryId, dto));
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsValid_ShouldReturnTrue()
        {
            // Arrange
            int libraryId = 1;
            var existingLibrary = new Library { Id = libraryId, Name = "Library to be deleted", CNPJ = "123456", Phone = "111111111" };

            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId)).ReturnsAsync(existingLibrary);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _libraryService.RemoveAsync(libraryId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.RemoveAsync(invalidId));
            Assert.Contains("O parâmetro Id deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task RemoveAsync_WhenLibraryNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int libraryId = 1;

            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId)).ReturnsAsync((Library)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.RemoveAsync(libraryId));
            Assert.Contains("Nenhuma Biblioteca foi encontrada com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int libraryId = 1;
            var existingLibrary = new Library { Id = libraryId, Name = "Library to be deleted", CNPJ = "123456", Phone = "111111111" };

            _mockUnitOfWork.Setup(uow => uow.Libraries.GetByIdAsync(libraryId)).ReturnsAsync(existingLibrary);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _libraryService.RemoveAsync(libraryId));
            Assert.Contains("Não foi possível deletar a Biblioteca. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }
    }
}
