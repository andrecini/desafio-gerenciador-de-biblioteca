using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.ValidateVerificationCode
{
    public class ValidateVerificationCodeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ValidateVerificationCodeCommand, CustomResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<bool>> Handle(ValidateVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<ValidateVerificationCodeCommandValidator, ValidateVerificationCodeCommand>(request);

            var codes = await _unitOfWork.VerificationCodes.FindAsync(x => x.UserId == request.UserId);
            
            var lastCode = codes.OrderByDescending(x => x.ValidTo).FirstOrDefault() ??
                throw new CustomException("Não foi possível encontrar nenhum Código de Autenticação para esse usuário. Tente novamente mais tarde!", HttpStatusCode.NotFound);

            if(lastCode.ValidTo < DateTime.Now) 
                throw new CustomException("O Código de Autenticação falhou. Tente novamente!", HttpStatusCode.NotFound);

            return lastCode.Code == request.Code ?
                new CustomResponse<bool>(true, "Código de Autenticação validado com sucesso!") :
                throw new CustomException("Código de Autenticação inválido. Tente novamente!", HttpStatusCode.NotFound);
        }
    }
}
