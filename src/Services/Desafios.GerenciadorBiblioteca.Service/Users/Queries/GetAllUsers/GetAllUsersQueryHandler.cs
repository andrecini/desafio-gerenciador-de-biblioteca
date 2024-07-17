﻿using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllUsersQueryValidator, GetAllUsersQuery>(request);

            var data = await _unitOfWork.Users.GetAllAsync();

            var viewModels = _mapper.Map<IEnumerable<UserViewModel>>(data);

            var paginatedViewModels = viewModels.Take(request.Size).Skip(request.Page);

            return paginatedViewModels;
        }
    }
}
