using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.Models.Filters;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Services;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Moq;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Tests.Application.Services
{
    public class LoanServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IInventoryService> _mockInventoryService;
        private readonly Mock<IBookService> _mockBookService;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockInventoryService = new Mock<IInventoryService>();
            _mockBookService = new Mock<IBookService>();
            _mockUserService = new Mock<IUserService>();
            _mockMapper = new Mock<IMapper>();
            _loanService = new LoanService(_mockUnitOfWork.Object, _mockInventoryService.Object, _mockBookService.Object, _mockUserService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenLoansExist_ShouldReturnAllLoans()
        {
            // Arrange
            var loans = new List<Loan>
            {
                new Loan { Id = 1, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false },
                new Loan { Id = 2, UserId = 2, InventoryId = 2, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = true }
            };
            _mockUnitOfWork.Setup(uow => uow.Loans.GetAllAsync()).ReturnsAsync(loans);

            // Act
            var result = await _loanService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(loans.Count, result.Count());
            Assert.Equal(loans, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsValid_ShouldReturnLoan()
        {
            // Arrange
            int loanId = 1;
            var loan = new Loan { Id = loanId, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false };
            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync(loan);

            // Act
            var result = await _loanService.GetByIdAsync(loanId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(loanId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.GetByIdAsync(invalidId));
            Assert.Contains("O parâmetro Empréstimo deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task GetByFilterAsync_WhenFiltersMatch_ShouldReturnFilteredLoans()
        {
            // Arrange
            var filter = new LoanFilter { UserId = 1 };
            var loans = new List<Loan>
            {
                new Loan { Id = 1, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false },
                new Loan { Id = 2, UserId = 2, InventoryId = 2, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = true }
            };
            _mockUnitOfWork.Setup(uow => uow.Loans.GetAllAsync()).ReturnsAsync(loans);

            // Act
            var result = await _loanService.GetByFilterAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result.First().UserId);
        }

        [Fact]
        public async Task GetByUserAsync_WhenUserIdIsValid_ShouldReturnLoans()
        {
            // Arrange
            int userId = 1;
            var loans = new List<Loan>
        {
            new Loan { Id = 1, UserId = userId, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false },
            new Loan { Id = 2, UserId = userId, InventoryId = 2, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = true }
        };
            _mockUnitOfWork.Setup(uow => uow.Loans.FindAsync(x => x.UserId == userId)).ReturnsAsync(loans);

            // Act
            var result = await _loanService.GetByUserAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, x => Assert.Equal(userId, x.UserId));
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsValid_ShouldReturnAddedLoan()
        {
            // Arrange
            var dto = new LoanInputModel(1, 1, DateTime.Now, DateTime.Now.AddDays(7));
            var loan = new Loan { Id = 1, UserId = 1, InventoryId = 1, LoanDate = dto.LoanDate, LoanValidity = dto.LoanValidity, Returned = false };
            _mockMapper.Setup(m => m.Map<Loan>(dto)).Returns(loan);
            _mockUnitOfWork.Setup(uow => uow.Loans.AddAsync(It.IsAny<Loan>())).ReturnsAsync(loan);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);
            _mockInventoryService.Setup(s => s.UpdateStatusAsync(dto.InventoryId, false)).ReturnsAsync(new Inventory { Id = dto.InventoryId, Available = false });

            // Act
            var result = await _loanService.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(loan.UserId, result.UserId);
            Assert.Equal(loan.InventoryId, result.InventoryId);
            Assert.False(result.Returned);
        }

        [Fact]
        public async Task AddAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            var dto = new LoanInputModel(1, 1, DateTime.Now, DateTime.Now.AddDays(7));
            var loan = new Loan { Id = 1, UserId = 1, InventoryId = 1, LoanDate = dto.LoanDate, LoanValidity = dto.LoanValidity, Returned = false };
            _mockMapper.Setup(m => m.Map<Loan>(dto)).Returns(loan);
            _mockUnitOfWork.Setup(uow => uow.Loans.AddAsync(It.IsAny<Loan>())).ReturnsAsync(loan);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.AddAsync(dto));
            Assert.Contains("Não foi possível adicionar o Empréstimo. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsValid_ShouldReturnUpdatedLoan()
        {
            // Arrange
            int loanId = 1;
            var dto = new LoanInputModel(1, 1, DateTime.Now, DateTime.Now.AddDays(7));
            var existingLoan = new Loan { Id = loanId, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false };

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync(existingLoan);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _loanService.UpdateAsync(loanId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.UserId, result.UserId);
            Assert.Equal(dto.InventoryId, result.InventoryId);
            Assert.Equal(dto.LoanDate, result.LoanDate);
            Assert.Equal(dto.LoanValidity, result.LoanValidity);
        }

        [Fact]
        public async Task UpdateAsync_WhenLoanNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int loanId = 1;
            var dto = new LoanInputModel(1, 1, DateTime.Now, DateTime.Now.AddDays(7));

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync((Loan)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.UpdateAsync(loanId, dto));
            Assert.Contains("Nenhum Emrpéstimo foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int loanId = 1;
            var dto = new LoanInputModel(1, 1, DateTime.Now, DateTime.Now.AddDays(7));
            var existingLoan = new Loan { Id = loanId, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false };

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync(existingLoan);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.UpdateAsync(loanId, dto));
            Assert.Contains("Não foi possível atualizar o Empréstimo. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateStatusAsync_WhenIdIsValid_ShouldReturnUpdatedLoan()
        {
            // Arrange
            int loanId = 1;
            bool returned = true;
            var existingLoan = new Loan { Id = loanId, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false };

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync(existingLoan);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);
            _mockInventoryService.Setup(s => s.UpdateStatusAsync(existingLoan.InventoryId, returned)).ReturnsAsync(new Inventory { Id = existingLoan.InventoryId, Available = returned });

            // Act
            var result = await _loanService.UpdateStatusAsync(loanId, returned);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(returned, result.Returned);
        }

        [Fact]
        public async Task UpdateStatusAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;
            bool returned = true;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.UpdateStatusAsync(invalidId, returned));
            Assert.Contains("O parâmetro Empréstimo deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task UpdateStatusAsync_WhenLoanNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int loanId = 1;
            bool returned = true;

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync((Loan)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.UpdateStatusAsync(loanId, returned));
            Assert.Contains("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateStatusAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int loanId = 1;
            bool returned = true;
            var existingLoan = new Loan { Id = loanId, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false };

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync(existingLoan);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.UpdateStatusAsync(loanId, returned));
            Assert.Contains("Não foi possível atualizar o Empréstimo. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsValid_ShouldReturnTrue()
        {
            // Arrange
            int loanId = 1;
            var existingLoan = new Loan { Id = loanId, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false };

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync(existingLoan);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _loanService.RemoveAsync(loanId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.RemoveAsync(invalidId));
            Assert.Contains("O parâmetro Empréstimo deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task RemoveAsync_WhenLoanNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int loanId = 1;

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync((Loan)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.RemoveAsync(loanId));
            Assert.Contains("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int loanId = 1;
            var existingLoan = new Loan { Id = loanId, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false };

            _mockUnitOfWork.Setup(uow => uow.Loans.GetByIdAsync(loanId)).ReturnsAsync(existingLoan);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _loanService.RemoveAsync(loanId));
            Assert.Contains("Não foi possível deletar o Empréstimo. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task GetLoanDetailsByLibraryAsync_WhenLoansExist_ShouldReturnLoanDetails()
        {
            // Arrange
            int libraryId = 1;
            var inventories = new List<Inventory>
            {
                new Inventory { Id = 1, LibraryId = libraryId, BookId = 1, Available = true },
                new Inventory { Id = 2, LibraryId = libraryId, BookId = 2, Available = false }
            };
            var loans = new List<Loan>
            {
                new Loan { Id = 1, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false },
                new Loan { Id = 2, UserId = 2, InventoryId = 2, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = true }
            };
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1" },
                new Book { Id = 2, Title = "Book 2" }
            };
            var users = new List<UserViewModel>
            {
                new UserViewModel(1, "User 1", "teste@email.com", "11988887777", Roles.Common),
                new UserViewModel(2, "User 2", "teste@email.com", "11988887777", Roles.Common)
            };

            _mockInventoryService.Setup(service => service.GetByLibraryAsync(libraryId)).ReturnsAsync(inventories);
            _mockUnitOfWork.Setup(uow => uow.Loans.GetAllAsync()).ReturnsAsync(loans);
            _mockBookService.Setup(service => service.GetAllAsync()).ReturnsAsync(books);
            _mockUserService.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _loanService.GetLoanDetailsByLibraryAsync(libraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetFilteredLoanDetailsAsync_WhenFiltersMatch_ShouldReturnFilteredLoanDetails()
        {
            // Arrange
            var filter = new LoanDetailsFilter
            {
                LibraryId = 1,
                BookName = "Book",
                UserName = "User",
                LoanFilter = new LoanFilter { Status = LoanStatus.Returned }
            };

            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1" },
                new Book { Id = 2, Title = "Book 2" }
            };
            var inventories = new List<Inventory>
            {
                new Inventory { Id = 1, LibraryId = 1, BookId = 1, Available = true },
                new Inventory { Id = 2, LibraryId = 1, BookId = 2, Available = false }
            };
            var users = new List<UserViewModel>
            {
                new UserViewModel (1, "User 1", "teste@email.com", "11988887777", Roles.Common),
                new UserViewModel (2, "User 2", "teste@email.com", "11988887777", Roles.Common)
            };
            var loans = new List<Loan>
            {
                new Loan { Id = 1, UserId = 1, InventoryId = 1, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = false },
                new Loan { Id = 2, UserId = 2, InventoryId = 2, LoanDate = DateTime.Now, LoanValidity = DateTime.Now.AddDays(7), Returned = true }
            };

            _mockBookService.Setup(service => service.GetByFilterAsync(It.IsAny<BookFilter>())).ReturnsAsync(books);
            _mockInventoryService.Setup(service => service.GetByLibraryAsync(filter.LibraryId)).ReturnsAsync(inventories);
            _mockUserService.Setup(service => service.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(users);
            _mockUnitOfWork.Setup(uow => uow.Loans.GetAllAsync()).ReturnsAsync(loans);

            // Act
            var result = await _loanService.GetFilteredLoanDetailsAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }
    }
}
