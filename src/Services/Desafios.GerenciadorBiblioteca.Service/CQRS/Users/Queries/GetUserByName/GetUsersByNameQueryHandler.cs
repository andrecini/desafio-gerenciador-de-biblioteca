using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByName
{
    public class GetUsersByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUsersByNameQuery, CustomResponse<IEnumerable<UserViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<UserViewModel>>> Handle(GetUsersByNameQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetUsersByNameQueryValidator, GetUsersByNameQuery>(request);

            var users = await _unitOfWork.Users.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Name))
                users = users.Where(x => x.Name.Contains(request.Name, StringComparison.CurrentCultureIgnoreCase));

            var viewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

            var paginatedViewModels = viewModels.Take(request.Size).Skip(request.Page);

            return new CustomResponse<IEnumerable<UserViewModel>>(paginatedViewModels, "Usuários recuperados com sucesso!", viewModels.Count());
        }
    }
}