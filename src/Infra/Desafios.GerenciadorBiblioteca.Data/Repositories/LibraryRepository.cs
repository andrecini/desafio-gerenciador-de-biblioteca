using Dapper;
using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories.Base;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text;

namespace Desafios.GerenciadorBiblioteca.Data.Repositories
{
    public class LibraryRepository(LibraryDbContext context, IConfiguration configuration) : GenericRepository<Library, int>(context), ILibraryRepository
    {
        private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<Library>> GetLibrariesByBook(int bookId)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT 
	                        l.Id AS Id,
                            l.Name AS Name,
                            l.CNPJ AS CNPJ,
                            l.Phone AS Phone
                        FROM Libraries AS l
                        INNER JOIN Inventories AS i
                        ON i.Id = i.LibraryId
                        WHERE i.BookId = @BookId");

            var data = await connection.QueryAsync<Library>(sb.ToString(), new { BookId = bookId });

            return data;
        }

        public async Task<IEnumerable<Library>> GetLibrariesByBookFiltered(int bookId, string name)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT 
	                        l.Id AS Id,
                            l.Name AS Name,
                            l.CNPJ AS CNPJ,
                            l.Phone AS Phone
                        FROM Libraries AS l
                        INNER JOIN Inventories AS i
                        ON i.Id = i.LibraryId
                        WHERE i.BookId = @BookId");

            if (!string.IsNullOrEmpty(name))
                sb.AppendLine("AND LOWER(l.Name) LIKE LOWER(@Name)");

            var data = await connection.QueryAsync<Library>(sb.ToString(), new { BookId = bookId, Name = $"%{name}%" });

            return data;
        }
    }
}
