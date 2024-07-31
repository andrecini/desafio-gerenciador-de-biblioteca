using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserByEmailQuery, CustomResponse<UserViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<UserViewModel>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetUserByEmailQueryValidator, GetUserByEmailQuery>(request);

            var data = await _unitOfWork.Users.FindAsync(x => x.Email == request.Email);

            var viewModel = _mapper.Map<UserViewModel>(data.FirstOrDefault()) ?? 
                throw new CustomException("Nenhum usuário cadastrado para esse Email!", HttpStatusCode.NotFound);

            return new CustomResponse<UserViewModel>(viewModel, "Usuário recuperado com sucesso!");

        }
    }
}
