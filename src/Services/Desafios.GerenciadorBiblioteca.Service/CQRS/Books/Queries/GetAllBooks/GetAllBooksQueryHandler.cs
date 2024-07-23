using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetAllBooks
{
    public class GetAllBooksQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllBooksQuery, CustomResponse<IEnumerable<Book>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllBooksQueryValidator, GetAllBooksQuery>(request);

            var data = await _unitOfWork.Books.GetAllAsync();
            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Book>>(paginatedData, "Livros recuperados com sucesso!", data.Count());
        }
    }
}
