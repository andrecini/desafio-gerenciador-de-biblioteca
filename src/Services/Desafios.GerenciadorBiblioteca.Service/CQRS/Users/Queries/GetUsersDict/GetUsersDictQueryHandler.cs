using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUsersDict
{
    public class GetUsersDictQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUsersDictQuery, CustomResponse<Dictionary<int, string>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<Dictionary<int, string>>> Handle(GetUsersDictQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Users.GetAllAsync();

            Dictionary<int, string> dict = [];

            foreach (var user in data)
            {
                dict.Add(user.Id, user.Name);
            }

            return new CustomResponse<Dictionary<int, string>>(dict, "dicionário de Usuários recuperados com sucesso!");
        }
    }
}
