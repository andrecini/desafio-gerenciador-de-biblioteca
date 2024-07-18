using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetLibraryByName
{
    public class GetLibrariesByNameQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLibrariesByNameQuery, IEnumerable<Library>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Library>> Handle(GetLibrariesByNameQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLibrariesByNameQueryValidator, GetLibrariesByNameQuery>(request);

            var data = await _unitOfWork.Libraries.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Name))
                return data.Where(x => x.Name.Contains(request.Name, StringComparison.CurrentCultureIgnoreCase));

            var paginatedData = data.Paginate(request.Page, request.Size);

            return paginatedData;
        }
    }
}
