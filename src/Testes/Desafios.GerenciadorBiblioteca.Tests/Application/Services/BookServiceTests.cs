using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Moq;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Tests.Application.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IInventoryService> _mockInventoryService;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockInventoryService = new Mock<IInventoryService>();
            _bookService = new BookService(_mockUnitOfWork.Object, _mockMapper.Object, _mockInventoryService.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenBooksExist_ShouldReturnAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }
            };

            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(books.Count, result.Count());
            Assert.Equal(books, result);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoBooksExist_ShouldReturnEmptyList()
        {
            // Arrange
            var books = new List<Book>();

            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_WhenExceptionOccurs_ShouldThrowException()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _bookService.GetAllAsync());
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsValid_ShouldReturnBook()
        {
            // Arrange
            int bookId = 1;
            var book = new Book { Id = bookId, Title = "Book 1", Author = "Author 1" };

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId))
                           .ReturnsAsync(book);

            // Act
            var result = await _bookService.GetByIdAsync(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _bookService.GetByIdAsync(invalidId));
        }

        [Fact]
        public async Task GetByIdAsync_WhenBookDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            int bookId = 1;

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId))
                           .ReturnsAsync((Book)null);

            // Act
            var result = await _bookService.GetByIdAsync(bookId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenExceptionOccurs_ShouldThrowException()
        {
            // Arrange
            int bookId = 1;

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId))
                           .ThrowsAsync(new System.Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _bookService.GetByIdAsync(bookId));
        }

        [Fact]
        public async Task GetByFilterAsync_WhenFilterMatchesBooks_ShouldReturnFilteredBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "111", Year = 2020 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", ISBN = "222", Year = 2021 }
            };
            var filter = new BookFilter { Title = "Book 1" };

            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetByFilterAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Book 1", result.First().Title);
        }

        [Fact]
        public async Task GetByFilterAsync_WhenNoBooksMatchFilter_ShouldReturnEmptyList()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "111", Year = 2020 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", ISBN = "222", Year = 2021 }
            };
            var filter = new BookFilter { Title = "Nonexistent" };

            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetByFilterAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByFilterAsync_WhenExceptionOccurs_ShouldThrowException()
        {
            // Arrange
            var filter = new BookFilter { Title = "Book 1" };

            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ThrowsAsync(new System.Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _bookService.GetByFilterAsync(filter));
        }

        [Fact]
        public async Task GetByFilterAsync_WhenCalled_ShouldApplyFiltersCorrectly()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "111", Year = 2020 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", ISBN = "222", Year = 2021 },
                new Book { Id = 3, Title = "Another Book", Author = "Author 3", ISBN = "333", Year = 2020 }
            };
            var filter = new BookFilter { Author = "Author 1" };

            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetByFilterAsync(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Author 1", result.First().Author);
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsValid_ShouldReturnAddedBook()
        {
            // Arrange
            var dto = new BookDTO("New Book", "Author", "123456", 2022);
            var book = new Book { Id = 1, Title = "New Book", Author = "Author", ISBN = "123456", Year = 2022 };
            _mockMapper.Setup(m => m.Map<Book>(dto)).Returns(book);
            _mockUnitOfWork.Setup(uow => uow.Books.AddAsync(It.IsAny<Book>())).ReturnsAsync(book);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _bookService.AddAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Author, result.Author);
        }

        [Fact]
        public async Task AddAsync_WhenDtoIsNull_ShouldThrowCustomException()
        {
            // Arrange
            BookDTO dto = null;

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _bookService.AddAsync(dto));
        }

        [Fact]
        public async Task AddAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            var dto = new BookDTO("New Book", "Author", "123456", 2022);
            var book = new Book { Id = 1, Title = "New Book", Author = "Author", ISBN = "123456", Year = 2022 };
            _mockMapper.Setup(m => m.Map<Book>(dto)).Returns(book);
            _mockUnitOfWork.Setup(uow => uow.Books.AddAsync(It.IsAny<Book>())).ReturnsAsync(book);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _bookService.AddAsync(dto));
            Assert.Equal("Não foi possível adicionar o Livro. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsValid_ShouldReturnUpdatedBook()
        {
            // Arrange
            int bookId = 1;
            var dto = new BookDTO("Updated Book", "Updated Author", "654321", 2023);
            var existingBook = new Book { Id = bookId, Title = "Old Book", Author = "Old Author", ISBN = "123456", Year = 2020 };

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId)).ReturnsAsync(existingBook);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _bookService.UpdateAsync(bookId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Title, result.Title);
            Assert.Equal(dto.Author, result.Author);
            Assert.Equal(dto.ISBN, result.ISBN);
            Assert.Equal(dto.Year, result.Year);
        }

        [Fact]
        public async Task UpdateAsync_WhenDtoIsNull_ShouldThrowCustomException()
        {
            // Arrange
            int bookId = 1;
            BookDTO dto = null;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _bookService.UpdateAsync(bookId, dto));
            Assert.Equal("O parâmetro Livro não pode ser nulo.", exception.ErrorDetails);
        }

        [Fact]
        public async Task UpdateAsync_WhenBookNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int bookId = 1;
            var dto = new BookDTO("Updated Book", "Updated Author", "654321", 2023);

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _bookService.UpdateAsync(bookId, dto));
            Assert.Equal("Nenhum Livro foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int bookId = 1;
            var dto = new BookDTO("Updated Book", "Updated Author", "654321", 2023);
            var existingBook = new Book { Id = bookId, Title = "Old Book", Author = "Old Author", ISBN = "123456", Year = 2020 };

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId)).ReturnsAsync(existingBook);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _bookService.UpdateAsync(bookId, dto));
            Assert.Equal("Não foi possível alterar o Livro. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task UpdateAsync_WhenValidationFails_ShouldThrowValidationException()
        {
            // Arrange
            int bookId = 1;
            var dto = new BookDTO("Invalid Book", "Author", "123B56", 2022);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _bookService.UpdateAsync(bookId, dto));
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsValid_ShouldReturnTrue()
        {
            // Arrange
            int bookId = 1;
            var existingBook = new Book { Id = bookId, Title = "Book to be deleted", Author = "Author", ISBN = "123456", Year = 2020 };

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId)).ReturnsAsync(existingBook);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _bookService.RemoveAsync(bookId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveAsync_WhenIdIsInvalid_ShouldThrowCustomException()
        {
            // Arrange
            int invalidId = -1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _bookService.RemoveAsync(invalidId));
            Assert.Equal("O parâmetro Id deve ser maior ou igual a 1.", exception.ErrorDetails);
        }

        [Fact]
        public async Task RemoveAsync_WhenBookNotFound_ShouldThrowCustomException()
        {
            // Arrange
            int bookId = 1;

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId)).ReturnsAsync((Book)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _bookService.RemoveAsync(bookId));
            Assert.Equal("Nenhum Livro foi encontrado com essas informações. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveAsync_WhenSaveFails_ShouldThrowCustomException()
        {
            // Arrange
            int bookId = 1;
            var existingBook = new Book { Id = bookId, Title = "Book to be deleted", Author = "Author", ISBN = "123456", Year = 2020 };

            _mockUnitOfWork.Setup(uow => uow.Books.GetByIdAsync(bookId)).ReturnsAsync(existingBook);
            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => _bookService.RemoveAsync(bookId));
            Assert.Equal("Não foi possível deletar o Livro. Tente novamente!", exception.ErrorDetails);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
        }

        [Fact]
        public async Task GetBooksDetailsByLibraryAsync_WhenBooksExist_ShouldReturnBookDetails()
        {
            // Arrange
            int libraryId = 1;
            var inventories = new List<Inventory>
            {
                new Inventory { Id = 1, BookId = 1, Available = true },
                new Inventory { Id = 2, BookId = 2, Available = false }
            };
                var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }
            };

            _mockInventoryService.Setup(service => service.GetByLibraryAsync(libraryId))
                                 .ReturnsAsync(inventories);
            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetBooksDetailsByLibraryAsync(libraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.BookDetails.Id == 1 && dto.InventoryId == 1 && dto.Available);
            Assert.Contains(result, dto => dto.BookDetails.Id == 2 && dto.InventoryId == 2 && !dto.Available);
        }

        [Fact]
        public async Task GetBooksDetailsByLibraryAsync_WhenNoBooksExist_ShouldReturnEmptyList()
        {
            // Arrange
            int libraryId = 1;
            var inventories = new List<Inventory>();
            var books = new List<Book>();

            _mockInventoryService.Setup(service => service.GetByLibraryAsync(libraryId))
                                 .ReturnsAsync(inventories);
            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetBooksDetailsByLibraryAsync(libraryId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBooksDetailsFilteredAsync_WhenBooksExist_ShouldReturnFilteredBookDetails()
        {
            // Arrange
            int libraryId = 1;
            var filter = new BookFilter { Title = "Book" };
            var inventories = new List<Inventory>
            {
                new Inventory { Id = 1, BookId = 1, Available = true },
                new Inventory { Id = 2, BookId = 2, Available = false }
            };
                var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }
            };

            _mockInventoryService.Setup(service => service.GetByLibraryAsync(libraryId))
                                 .ReturnsAsync(inventories);
            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetBooksDetailsFilteredAsync(libraryId, filter, 0);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, dto => dto.BookDetails.Id == 1 && dto.InventoryId == 1 && dto.Available);
            Assert.Contains(result, dto => dto.BookDetails.Id == 2 && dto.InventoryId == 2 && !dto.Available);
        }

        [Fact]
        public async Task GetBooksDetailsFilteredAsync_WhenNoBooksMatchFilter_ShouldReturnEmptyList()
        {
            // Arrange
            int libraryId = 1;
            var filter = new BookFilter { Title = "Nonexistent" };
            var inventories = new List<Inventory>();
            var books = new List<Book>();

            _mockInventoryService.Setup(service => service.GetByLibraryAsync(libraryId))
                                 .ReturnsAsync(inventories);
            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetBooksDetailsFilteredAsync(libraryId, filter, 0);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBooksDetailsFilteredAsync_WhenAvailableFilterIsApplied_ShouldReturnAvailableBooksOnly()
        {
            // Arrange
            int libraryId = 1;
            var filter = new BookFilter { Title = "Book" };
            var inventories = new List<Inventory>
            {
                new Inventory { Id = 1, BookId = 1, Available = true },
                new Inventory { Id = 2, BookId = 2, Available = false }
            };
                var books = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2" }
            };

            _mockInventoryService.Setup(service => service.GetByLibraryAsync(libraryId))
                                 .ReturnsAsync(inventories);
            _mockUnitOfWork.Setup(uow => uow.Books.GetAllAsync())
                           .ReturnsAsync(books);

            // Act
            var result = await _bookService.GetBooksDetailsFilteredAsync(libraryId, filter, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, dto => dto.BookDetails.Id == 1 && dto.InventoryId == 1 && dto.Available);
        }
    }
}
