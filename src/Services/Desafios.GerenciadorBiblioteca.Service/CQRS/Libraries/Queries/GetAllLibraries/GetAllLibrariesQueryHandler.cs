using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetAllLibraries
{
    public class GetAllLibrariesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllLibrariesQuery, CustomResponse<IEnumerable<Library>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Library>>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllLibrariesValidator, GetAllLibrariesQuery>(request);

            var data = await _unitOfWork.Libraries.GetAllAsync();

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Library>>(paginatedData, "Bibliotecas recuperadas com sucesso!", data.Count());
        }
    }
}
