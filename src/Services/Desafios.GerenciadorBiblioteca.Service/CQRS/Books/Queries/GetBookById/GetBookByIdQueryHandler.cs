using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBookByIdQuery, CustomResponse<Book>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBookByIdQueryValidator, GetBookByIdQuery>(request);

            var data = await _unitOfWork.Books.GetByIdAsync(request.Id);

            return new CustomResponse<Book>(data, "Livro recuperado com sucesso!");
        }
    }
}
