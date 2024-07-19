using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksByFilter
{
    public class GetBooksByFilterQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBooksByFilterQuery, CustomResponse<IEnumerable<Book>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Book>>> Handle(GetBooksByFilterQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBooksByFilterQueryValidator, GetBooksByFilterQuery>(request);

            var data = await _unitOfWork.Books.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Title))
                data = data.Where(x => x.Title.Contains(request.Title, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(request.Author))
                data = data.Where(x => x.Author.Contains(request.Author, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(request.ISBN))
                data = data.Where(x => x.ISBN.Contains(request.ISBN, StringComparison.CurrentCultureIgnoreCase));
            if (request.Year > 0)
                data = data.Where(x => x.Year == request.Year);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Book>>(paginatedData, "Livros recuperados com sucesso!", data.Count());

        }
    }
}
