using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBookById
{
    public class GetBookByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBookByIdQueryValidator, GetBookByIdQuery>(request);

            var data = await _unitOfWork.Books.GetByIdAsync(request.Id;

            return data;
        }
    }
}
