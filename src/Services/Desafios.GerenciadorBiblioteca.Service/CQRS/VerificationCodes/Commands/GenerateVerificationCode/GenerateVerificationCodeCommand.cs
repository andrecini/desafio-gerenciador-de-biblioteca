using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.GenerateVerificationCode
{
    public record GenerateVerificationCodeCommand(int UserId) : IRequest<CustomResponse<VerificationCode>>;
}
