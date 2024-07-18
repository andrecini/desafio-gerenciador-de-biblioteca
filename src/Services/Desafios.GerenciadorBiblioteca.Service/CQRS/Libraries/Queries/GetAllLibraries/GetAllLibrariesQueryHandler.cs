using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetAllLibraries
{
    public class GetAllLibrariesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllLibrariesQuery, IEnumerable<Library>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Library>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllLibrariesQueryValidator, GetAllLibrariesQuery>(request);

            var data = await _unitOfWork.Libraries.GetAllAsync();

            var paginatedData = data.Paginate(request.Page, request.Size);

            return paginatedData;
        }
    }
}
