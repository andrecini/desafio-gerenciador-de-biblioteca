using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.ValidateVerificationCode
{
    public record ValidateVerificationCodeCommand(int UserId, string Code) : IRequest<CustomResponse<bool>>;
}
