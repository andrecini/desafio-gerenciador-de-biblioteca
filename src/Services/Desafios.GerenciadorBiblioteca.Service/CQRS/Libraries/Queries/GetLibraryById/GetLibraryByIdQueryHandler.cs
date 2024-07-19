using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibraryById
{
    public class GetLibraryByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLibraryByIdQuery, CustomResponse<Library>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<Library>> Handle(GetLibraryByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLibraryByIdQueryValidator, GetLibraryByIdQuery>(request);

            var data = await _unitOfWork.Libraries.GetByIdAsync(request.Id);

            return new CustomResponse<Library>(data, "Biblioteca recuperada com sucesso!");
        }
    }
}
