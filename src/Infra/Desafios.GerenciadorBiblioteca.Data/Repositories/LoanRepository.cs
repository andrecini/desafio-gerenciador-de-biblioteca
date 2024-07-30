using Dapper;
using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories.Base;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Desafios.GerenciadorBiblioteca.Data.Repositories
{
    public class LoanRepository(LibraryDbContext context, IConfiguration configuration) : GenericRepository<Loan, int>(context), ILoanRepository
    {
        private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<LoanDetailsQueryResult>> GetLoanDetailsFilteredByLibraryAsync(LoanDetailsFilteredByLibraryQueryRequest request)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT DISTINCT
	                            l.Id AS Id,
                                l.InventoryId AS InventoryId,
                                l.UserId AS UserId,
	                            l.LoanDate AS LoanDate,
	                            l.LoanValidity AS LoanValidity,
	                            l.Returned AS Returned,
                                b.Title AS BookName,
                                u.Name AS Username
                            FROM Loans AS l
                            INNER JOIN Inventories AS i
                            ON l.InventoryId = i.BookId
                            INNER JOIN Books AS b
                            ON i.BookId = b.Id
                            INNER JOIN Users AS u
                            ON l.UserId = u.Id
                            WHERE i.LibraryId = @LibraryId");

            if (!string.IsNullOrEmpty(request.BookName))
                sb.AppendLine("AND LOWER(b.Title) LIKE LOWER(@BookName)");
            if (!string.IsNullOrEmpty(request.UserName))
                sb.AppendLine("AND LOWER(u.Name) LIKE LOWER(@UserName)");
            if (request.LoanDate != default)
                sb.AppendLine("AND CAST(l.LoanDate AS DATE) = CAST(@LoanDate AS DATE)");
            if (request.LoanValidity != default)
                sb.AppendLine("AND CAST(l.LoanValidity AS DATE) = CAST(@LoanValidity AS DATE)");
            if (request.Status > 0)
                sb.AppendLine("AND l.Returned = @Status");

            return await connection.QueryAsync<LoanDetailsQueryResult>(sb.ToString(), new
            {
                request.LibraryId,
                BookName = $"%{request.BookName}%",
                UserName = $"%{request.UserName}%",
                request.LoanDate,
                request.LoanValidity,
                Status = (int)request.Status == 2 ? 0 : 1,
            });
        }

        public async Task<IEnumerable<LoanDetailsQueryResult>> GetLoanDetailsByLibraryAsync(LoanDetailsQueryByLibraryRequest request)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT DISTINCT
	                            l.Id AS Id,
                                l.InventoryId AS InventoryId,
                                l.UserId AS UserId,
	                            l.LoanDate AS LoanDate,
	                            l.LoanValidity AS LoanValidity,
	                            l.Returned AS Returned,
                                b.Title AS BookName,
                                u.Name AS Username
                            FROM Loans AS l
                            INNER JOIN Inventories AS i
                            ON l.InventoryId = i.BookId
                            INNER JOIN Books AS b
                            ON i.BookId = b.Id
                            INNER JOIN Users AS u
                            ON l.UserId = u.Id
                            WHERE i.LibraryId = @LibraryId");

            return await connection.QueryAsync<LoanDetailsQueryResult>(sb.ToString(), request);
        }

        public async Task<IEnumerable<LoanDetailsQueryResult>> GetLoanDetailsByUserAsync(LoanDetailsQueryByUserRequest request)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT DISTINCT
	                            l.Id AS Id,
                                l.InventoryId AS InventoryId,
                                l.UserId AS UserId,
	                            l.LoanDate AS LoanDate,
	                            l.LoanValidity AS LoanValidity,
	                            l.Returned AS Returned,
                                b.Title AS BookName,
                                u.Name AS Username
                            FROM Loans AS l
                            INNER JOIN Inventories AS i
                            ON l.InventoryId = i.BookId
                            INNER JOIN Books AS b
                            ON i.BookId = b.Id
                            INNER JOIN Users AS u
                            ON l.UserId = u.Id
                            WHERE l.UserId = @UserId");

            return await connection.QueryAsync<LoanDetailsQueryResult>(sb.ToString(), request);
        }

        public async Task<IEnumerable<LoanDetailsQueryResult>> GetLoanDetailsFilteredByUserAsync(LoanDetailsFilteredByUserQueryRequest request)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT DISTINCT
	                            l.Id AS Id,
                                l.InventoryId AS InventoryId,
                                l.UserId AS UserId,
	                            l.LoanDate AS LoanDate,
	                            l.LoanValidity AS LoanValidity,
	                            l.Returned AS Returned,
                                b.Title AS BookName,
                                u.Name AS Username
                            FROM Loans AS l
                            INNER JOIN Inventories AS i
                            ON l.InventoryId = i.BookId
                            INNER JOIN Books AS b
                            ON i.BookId = b.Id
                            INNER JOIN Users AS u
                            ON l.UserId = u.Id
                            WHERE l.UserId = @UserId");

            if (!string.IsNullOrEmpty(request.BookName))
                sb.AppendLine("AND LOWER(b.Title) LIKE LOWER(@BookName)");
            if (request.LoanDate != default)
                sb.AppendLine("AND CAST(l.LoanDate AS DATE) = CAST(@LoanDate AS DATE)");
            if (request.LoanValidity != default)
                sb.AppendLine("AND CAST(l.LoanValidity AS DATE) = CAST(@LoanValidity AS DATE)");
            if (request.Status > 0)
                sb.AppendLine("AND l.Returned = @Status");

            return await connection.QueryAsync<LoanDetailsQueryResult>(sb.ToString(), new
            {
                request.UserId,
                BookName = $"%{request.BookName}%",
                request.LoanDate,
                request.LoanValidity,
                Status = (int)request.Status == 2 ? 0 : 1,
            });
        }

        public async Task<IEnumerable<OverdueLoansDetailsQueryResult>> GetOverdueLoanDetailsAsync(DateTime actualDate)
        {
            using SqlConnection connection = new(_connectionString);

            StringBuilder sb = new();

            sb.AppendLine(@"SELECT DISTINCT
	                            l.LoanDate AS LoanDate,
	                            l.LoanValidity AS LoanValidity,
                                b.Title AS BookName,
                                u.Name AS Username,
	                            u.Email As Email
                            FROM Loans AS l
                            INNER JOIN Inventories AS i
                            ON l.InventoryId = i.BookId
                            INNER JOIN Books AS b
                            ON i.BookId = b.Id
                            INNER JOIN Users AS u
                            ON l.UserId = u.Id
                            WHERE CAST(l.LoanValidity AS DATE) < CAST(@actualDate AS DATE)
                            AND l.Returned = 0");

            return await connection.QueryAsync<OverdueLoansDetailsQueryResult>(sb.ToString(), new { actualDate });
        }
    }
}
