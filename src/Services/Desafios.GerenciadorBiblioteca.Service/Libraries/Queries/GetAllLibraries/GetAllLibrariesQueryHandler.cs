using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetAllLibraries
{
    public class GetAllLibrariesQueryHandler : IRequestHandler<GetAllLibrariesQuery, IEnumerable<Library>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLibrariesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Library>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllLibrariesQueryValidator, GetAllLibrariesQuery>(request);

            var data = await _unitOfWork.Libraries.GetAllAsync();

            var paginatedData = data.Take(request.Size).Skip(request.Page);

            return paginatedData;
        }
    }
}
