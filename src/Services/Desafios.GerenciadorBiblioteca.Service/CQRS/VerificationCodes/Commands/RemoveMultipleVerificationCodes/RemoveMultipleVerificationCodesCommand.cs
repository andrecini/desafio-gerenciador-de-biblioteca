using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.RemoveMultipleVerificationCodes
{
    public record RemoveMultipleVerificationCodesCommand() : IRequest<CustomResponse<bool>>;
}
