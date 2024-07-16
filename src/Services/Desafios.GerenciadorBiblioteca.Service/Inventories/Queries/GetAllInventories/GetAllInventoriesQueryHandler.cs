using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetLibraryByName;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetAllInventories
{
    public class GetAllInventoriesQueryHandler : IRequestHandler<GetAllInventoriesQuery, IEnumerable<Inventory>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllInventoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Inventory>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllInventoriesQueryValidator, GetAllInventoriesQuery>(request);

            var data = await _unitOfWork.Inventories.GetAllAsync();

            var paginatedData = data.Take(request.Size).Skip(request.Size);

            return paginatedData;
        }
    }
}
