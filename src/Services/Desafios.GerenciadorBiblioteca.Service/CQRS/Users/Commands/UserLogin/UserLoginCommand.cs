﻿using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UserLogin
{
    public record UserLoginCommand(string Email, string Password) : IRequest<UserViewModel>;
}