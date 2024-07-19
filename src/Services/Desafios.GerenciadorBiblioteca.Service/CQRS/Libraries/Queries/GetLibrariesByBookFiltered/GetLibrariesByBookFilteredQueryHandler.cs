using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibrariesByBookFiltered
{
    public class GetLibrariesByBookFilteredQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLibrariesByBookFilteredQuery, CustomResponse<IEnumerable<Library>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Library>>> Handle(GetLibrariesByBookFilteredQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLibrariesByBookFilteredQueryValidator, GetLibrariesByBookFilteredQuery>(request);

            var data = await _unitOfWork.Libraries.GetLibrariesByBookFiltered(request.BookId, request.Name);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Library>>(paginatedData, "Bibliotecas recuperadas com sucesso!", data.Count());

        }
    }
}
