using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories.Base;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults;
using Dapper;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;

namespace Desafios.GerenciadorBiblioteca.Data.Repositories
{
    public class BookRepository(LibraryDbContext context, IConfiguration configuration) : GenericRepository<Book, int>(context), IBookRepository
    {
        private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<BookDetailsQueryResult>> GetBooksDetailsByLibraryAsync(BookDetailsQueryRequest request)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT 
	                        b.Id AS Id,
                            b.Title AS Title,
                            b.Author AS Author,
                            b.ISBN AS ISBN,
                            b.Year AS Year,
	                        i.Id AS InventoryId,
	                        i.Available AS Available
                        FROM Books AS b
                        INNER JOIN Inventories AS i
                        ON b.Id = i.BookId
                        WHERE i.LibraryId = @LibraryId");

            var data = await connection.QueryAsync<BookDetailsQueryResult>(sb.ToString(), request);

            return data;
        }

        public async Task<IEnumerable<BookDetailsQueryResult>> GetBooksDetailsByFilterAsync(BookDetailsFilteredQueryRequest request)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT 
	                        b.Id AS Id,
                            b.Title AS Title,
                            b.Author AS Author,
                            b.ISBN AS ISBN,
                            b.Year AS Year,
	                        i.Id AS InventoryId,
	                        i.Available AS Available
                        FROM Books AS b
                        INNER JOIN Inventories AS i
                        ON b.Id = i.BookId
                        WHERE i.LibraryId = @LibraryId");

            if (!string.IsNullOrEmpty(request.Title))
                sb.AppendLine("AND LOWER(b.Title) LIKE LOWER(@Title)");
            if (!string.IsNullOrEmpty(request.Author))
                sb.AppendLine("AND LOWER(b.Author) LIKE LOWER(@Author)");
            if (!string.IsNullOrEmpty(request.ISBN))
                sb.AppendLine("AND LOWER(b.ISBN) LIKE LOWER(@ISBN)");
            if (request.Year > 0)
                sb.AppendLine("AND b.Year = @Year");
            if (request.Available > 0)
                sb.AppendLine("AND i.Available = @Available");

            var data = await connection.QueryAsync<BookDetailsQueryResult>(sb.ToString(), request);

            return data;
        }
    }
}
