using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUsersQuery, CustomResponse<IEnumerable<UserViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllUsersQueryValidator, GetAllUsersQuery>(request);

            var data = await _unitOfWork.Users.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<UserViewModel>>(data);

            var paginatedViewModels = viewModels.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<UserViewModel>>(paginatedViewModels, "Usuários recuperados com sucesso!", data.Count());
        }
    }
}
