using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoryByLibrary
{
    public class GetInventoryByLibraryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetInventoryByLibraryQuery, IEnumerable<Inventory>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Inventory>> Handle(GetInventoryByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetInventoryByLibraryQueryValidator, GetInventoryByLibraryQuery>(request);

            var data = await _unitOfWork.Inventories.FindAsync(x => x.LibraryId == request.LibraryId);

            var paginatedData = data.Take(request.Size).Skip(request.Page);

            return paginatedData;
        }
    }
}
