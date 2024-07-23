using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserByIdQuery, CustomResponse<UserViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetUserByIdQueryValidator, GetUserByIdQuery>(request);

            var data = await _unitOfWork.Users.GetByIdAsync(request.Id);
            var viewModel = _mapper.Map<UserViewModel>(data);

            return new CustomResponse<UserViewModel>(viewModel, "Usuário recuperado com sucesso!");

        }
    }
}
