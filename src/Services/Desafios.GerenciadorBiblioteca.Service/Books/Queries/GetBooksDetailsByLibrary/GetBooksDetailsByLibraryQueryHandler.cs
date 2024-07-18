using Dapper;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByLibrary
{
    public class GetBooksDetailsByLibraryQueryHandler(IUnitOfWork unitOfWork, IInventoryService inventoryService, IConfiguration configuration) : IRequestHandler<GetBooksDetailsByLibraryQuery, IEnumerable<BookDetailsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IInventoryService _inventoryService = inventoryService;
        private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection");


        public async Task<IEnumerable<BookDetailsViewModel>> Handle(GetBooksDetailsByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBooksDetailsByLibraryQueryValidator, GetBooksDetailsByLibraryQuery>(request);

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

            var data = await connection.QueryAsync<BookDetailsViewModel>(sb.ToString(), request);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return paginatedData;
        }
    }
}
