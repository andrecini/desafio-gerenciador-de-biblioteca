using Dapper;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByFilter
{
    public class GetBooksDetailsByFilterQueryHandler(IUnitOfWork unitOfWork, IInventoryService inventoryService, IConfiguration configuration) : IRequestHandler<GetBooksDetailsByFilterQuery, IEnumerable<BookDetailsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IInventoryService _inventoryService = inventoryService;
        private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<IEnumerable<BookDetailsViewModel>> Handle(GetBooksDetailsByFilterQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBooksDetailsByFilterQueryValidator, GetBooksDetailsByFilterQuery>(request); 

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

            var data = await connection.QueryAsync<BookDetailsViewModel>(sb.ToString(), new {
                request.LibraryId,
                Title = $"%{request.Title}%",
                Author = $"%{request.Author}%",
                ISBN = $"%{request.ISBN}%",
                request.Year,
                Available = request.Available == 2 ? 0 : 1,
            });

            var paginatedData = data.Paginate(request.Page, request.Size);

            return paginatedData;
        }
    }
}