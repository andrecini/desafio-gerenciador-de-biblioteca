using Azure.Core;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.AddUser;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.RemoveUser;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUser;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetAllUsers;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserById;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByName;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUsersDict;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(ILogger<UsersController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<UsersController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int size)
        {
            GetAllUsersQuery request = new(page, size);
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GetUserByIdQuery request = new(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("dict")]
        public async Task<IActionResult> GetUsersDict()
        {
            var response = await _mediator.Send(new GetUsersDictQuery());

            return Ok(response);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetFiltered(GetUsersByNameQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserCommand request)
        {
            var response = await _mediator.Send(request);

            var locationUri = Url.Link(nameof(GetById), new { id = response.Data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            RemoveUserCommand request = new(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
