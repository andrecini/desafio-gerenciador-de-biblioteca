using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetAllUsersCount
{
    public class GetAllUsersCountQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUsersCountQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> Handle(GetAllUsersCountQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Users.GetAllAsync();
            return data.Count();
        }
    }
}
