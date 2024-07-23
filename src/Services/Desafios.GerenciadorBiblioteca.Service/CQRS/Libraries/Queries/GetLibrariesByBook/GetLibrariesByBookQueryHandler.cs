using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibrariesByBook
{
    public class GetLibrariesByBookQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLibrariesByBookQuery, CustomResponse<IEnumerable<Library>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Library>>> Handle(GetLibrariesByBookQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLibrariesByBookQueryValidator, GetLibrariesByBookQuery>(request);

            var data = await _unitOfWork.Libraries.GetLibrariesByBook(request.BookId);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Library>>(paginatedData, "Bibliotecas recuperadas com sucesso!", data.Count());

        }
    }
}
