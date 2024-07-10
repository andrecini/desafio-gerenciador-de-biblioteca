using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services;
using Moq;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace Desafios.GerenciadorBiblioteca.Tests.Application.Services
{
    public class InventoryServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly InventoryService _inventoryService;

        public InventoryServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _inventoryService = new InventoryService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenInventoriesExist_ShouldReturnAllInventories()
        {
            // Arrange
            var inventories = new List<Inventory>
        {
            new Inventory { Id = 1, LibraryId = 1, BookId = 1, Available = true },
            new Inventory { Id = 2, LibraryId = 2, BookId = 2, Available = false }
        };
            _mockUnitOfWork.Setup(uow => uow.Inventories.GetAllAsync())
                           .ReturnsAsync(inventories);

            // Act
            var result = await _inventoryService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(inventories.Count, result.Count());
            Assert.Equal(inventories, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsValid_ShouldReturnInventory()
        {
            // Arrange
            int inventoryId = 1;
            var inventory = new Inventory { Id = inventoryId, LibraryId = 1, BookId = 1, Available = true };
            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId))
                           .ReturnsAsync(inventory);

            // Act
            var result = await _inventoryService.GetByIdAsync(inventoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(inventoryId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.GetByIdAsync(invalidId));
            Assert.Equal("O parâmetro Id deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task GetByFilterAsync_WhenFiltersMatch_ShouldReturnFilteredInventories()
        {
            // Arrange
            var filter = new InventoryFilter { LibraryId = 1 };
            var inventories = new List<Inventory>
            {
                new Inventory { Id = 1, LibraryId = 1, BookId = 1, Available = true },
                new Inventory { Id = 2, LibraryId = 2, BookId = 2, Available = false }
            };
            _mockUnitOfWork.Setup(uow => uow.Inventories.GetAllAsync())
                           .ReturnsAsync(inventories);

            // Act
            var result = await _inventoryService.GetByFilterAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result.First().LibraryId);
        }

        [Fact]
        public async Task GetByLibraryAsync_WhenLibraryIdIsValid_ShouldReturnInventories()
        {
            // Arrange
            int libraryId = 1;
            var inventories = new List<Inventory>
        {
            new Inventory { Id = 1, LibraryId = 1, BookId = 1, Available = true },
            new Inventory { Id = 2, LibraryId = 1, BookId = 2, Available = false }
        };
            _mockUnitOfWork.Setup(uow => uow.Inventories.FindAsync(x => x.LibraryId == libraryId))
                           .ReturnsAsync(inventories);

            // Act
            var result = await _inventoryService.GetByLibraryAsync(libraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, x => Assert.Equal(libraryId, x.LibraryId));
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsValid_ShouldReturnAddedInventory()
        {
            // Arrange
            var dto = new InventoryDTO(1, 1);
            var inventory = new Inventory { Id = 1, LibraryId = 1, BookId = 1, Available = true };
            _mockMapper.Setup(m => m.Map<Inventory>(dto)).Returns(inventory);
            _mockUnitOfWork.Setup(uow => uow.Inventories.AddAsync(It.IsAny<Inventory>())).ReturnsAsync(inventory);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _inventoryService.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(inventory.LibraryId, result.LibraryId);
            Assert.Equal(inventory.BookId, result.BookId);
            Assert.True(result.Available);
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsNull_ShouldThrowCustomException()
        {
            // Arrange
            InventoryDTO dto = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.AddAsync(dto));
            Assert.Equal("O parâmetro Inventário não pode ser nulo.", exception.ErrorDetails);
        }

        [Fact]
        public async Task AddAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            var dto = new InventoryDTO(1, 1);
            var inventory = new Inventory { Id = 1, LibraryId = 1, BookId = 1, Available = true };
            _mockMapper.Setup(m => m.Map<Inventory>(dto)).Returns(inventory);
            _mockUnitOfWork.Setup(uow => uow.Inventories.AddAsync(It.IsAny<Inventory>())).ReturnsAsync(inventory);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.AddAsync(dto));
            Assert.Equal("Não foi possível adicionar o Inventário. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsValid_ShouldReturnUpdatedInventory()
        {
            // Arrange
            int inventoryId = 1;
            var dto = new InventoryDTO(1, 1);
            var existingInventory = new Inventory { Id = inventoryId, LibraryId = 1, BookId = 1, Available = true };

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync(existingInventory);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _inventoryService.UpdateAsync(inventoryId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.LibraryId, result.LibraryId);
            Assert.Equal(dto.BookId, result.BookId);
            Assert.True(result.Available);
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsNull_ShouldThrowCustomException()
        {
            // Arrange
            int inventoryId = 1;
            InventoryDTO dto = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.UpdateAsync(inventoryId, dto));
            Assert.Equal("O parâmetro Inventário não pode ser nulo.", exception.ErrorDetails);
        }

        [Fact]
        public async Task UpdateAsync_WhenInventoryNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int inventoryId = 1;
            var dto = new InventoryDTO(1, 1);

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync((Inventory)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.UpdateAsync(inventoryId, dto));
            Assert.Equal("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int inventoryId = 1;
            var dto = new InventoryDTO(1, 1);
            var existingInventory = new Inventory { Id = inventoryId, LibraryId = 1, BookId = 1, Available = true };

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync(existingInventory);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.UpdateAsync(inventoryId, dto));
            Assert.Equal("Não foi possível alterar o Inventário. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateStatusAsync_WhenIdIsValid_ShouldReturnUpdatedInventory()
        {
            // Arrange
            int inventoryId = 1;
            bool available = false;
            var existingInventory = new Inventory { Id = inventoryId, LibraryId = 1, BookId = 1, Available = true };

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync(existingInventory);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _inventoryService.UpdateStatusAsync(inventoryId, available);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(available, result.Available);
        }

        [Fact]
        public async Task UpdateStatusAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;
            bool available = false;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.UpdateStatusAsync(invalidId, available));
            Assert.Equal("O parâmetro Id deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task UpdateStatusAsync_WhenInventoryNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int inventoryId = 1;
            bool available = false;

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync((Inventory)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.UpdateStatusAsync(inventoryId, available));
            Assert.Equal("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateStatusAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int inventoryId = 1;
            bool available = false;
            var existingInventory = new Inventory { Id = inventoryId, LibraryId = 1, BookId = 1, Available = true };

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync(existingInventory);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.UpdateStatusAsync(inventoryId, available));
            Assert.Equal("Não foi possível alterar o Status do Inventário. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsValid_ShouldReturnTrue()
        {
            // Arrange
            int inventoryId = 1;
            var existingInventory = new Inventory { Id = inventoryId, LibraryId = 1, BookId = 1, Available = true };

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync(existingInventory);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _inventoryService.RemoveAsync(inventoryId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.RemoveAsync(invalidId));
            Assert.Equal("O parâmetro Id deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task RemoveAsync_WhenInventoryNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int inventoryId = 1;

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync((Inventory)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.RemoveAsync(inventoryId));
            Assert.Equal("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int inventoryId = 1;
            var existingInventory = new Inventory { Id = inventoryId, LibraryId = 1, BookId = 1, Available = true };

            _mockUnitOfWork.Setup(uow => uow.Inventories.GetByIdAsync(inventoryId)).ReturnsAsync(existingInventory);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _inventoryService.RemoveAsync(inventoryId));
            Assert.Equal("Não foi possível deletar o Inventário. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }
    }
}
