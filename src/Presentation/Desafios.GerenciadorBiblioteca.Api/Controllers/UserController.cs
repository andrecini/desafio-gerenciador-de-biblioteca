using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.AddUser;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.RemoveUser;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUser;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetAllUsers;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserById;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByName;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(ILogger<UserController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllUsersQuery request)
        {
            var data = await _mediator.Send(request);
            var response = new CustomResponse<IEnumerable<UserViewModel>>(data, "Usu�rios Recupedados com Sucesso!");

            return data.Any() ? Ok(data) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(GetUserByIdQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<UserViewModel>(data, "Usu�rio Recupedado com Sucesso!");

            return data != null ? Ok(data) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetFiltered(GetUsersByNameQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<UserViewModel>>(data, "Usu�rios Recupedados com Sucesso!");

            return data.Any() ? Ok(data) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<UserViewModel>(data, "Usu�rio Adicionado com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<UserViewModel>(data, "Usu�rio Atualizado com Sucesso!");

            return Ok(response);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Remove(int id, RemoveUserCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<bool>(data, "Usu�rio Removido com Sucesso!");

            return Ok(response);
        }
    }
}
