using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoriesByFilter
{
    public class GetInventoriesByFilterQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetInventoriesByFilterQuery, IEnumerable<Inventory>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Inventory>> Handle(GetInventoriesByFilterQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetInventoriesByFilterQueryValidator, GetInventoriesByFilterQuery>(request);

            var data = await _unitOfWork.Inventories.GetAllAsync();

            if (request.LibraryId > 0)
                data = data.Where(x => x.LibraryId == request.LibraryId);
            if (request.BookId > 0)
                data = data.Where(x => x.BookId == request.BookId);
            if (request.Status == InventoryStatus.Available)
                data = data.Where(x => x.Available == true);
            if (request.Status == InventoryStatus.Unavailable)
                data = data.Where(x => x.Available == false);

            var paginatedData = data.Take(request.Size).Skip(request.Page);

            return paginatedData;
        }
    }
}
