using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetAllInventoriesCount
{
    public class GetAllInventoriesCountQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllInventoriesCountQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> Handle(GetAllInventoriesCountQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Inventories.GetAllAsync();
            return data.Count();
        }
    }
}
