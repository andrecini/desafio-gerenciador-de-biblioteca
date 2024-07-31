using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.RemoveMultipleVerificationCodes
{
    public class RemoveMultipleVerificationCodesCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveMultipleVerificationCodesCommand, CustomResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<bool>> Handle(RemoveMultipleVerificationCodesCommand request, CancellationToken cancellationToken)
        {
            var verificationCodes = await _unitOfWork.VerificationCodes.FindAsync(x => x.ValidTo < DateTime.Now.AddDays(-7));

            if (verificationCodes == null || !verificationCodes.Any()) 
                return new CustomResponse<bool>(true, "Nenhum código encontrado.");

            _unitOfWork.VerificationCodes.RemoveRange(verificationCodes);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ?
                new CustomResponse<bool>(true, "Os Códigos de Verificação Expirados foram removidos com sucesso!") :
                throw new CustomException("Não foi possível deletar os Códigos de Verificação Expirados. tente novamente mais tarde!", HttpStatusCode.InternalServerError);
        }
    }
}
