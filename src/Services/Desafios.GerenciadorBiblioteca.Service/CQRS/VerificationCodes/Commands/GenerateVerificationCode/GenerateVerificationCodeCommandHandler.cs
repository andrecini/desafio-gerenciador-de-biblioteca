using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.GenerateVerificationCode
{
    public class GenerateVerificationCodeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<GenerateVerificationCodeCommand, CustomResponse<VerificationCode>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<VerificationCode>> Handle(GenerateVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GenerateVerificationCodeCommandValidator, GenerateVerificationCodeCommand>(request);

            VerificationCode entity = new()
            {
                UserId = request.UserId,
                ValidTo = DateTime.Now.AddMinutes(10),
                Code = GenerateCode()
            };

            var registeredEntity = await _unitOfWork.VerificationCodes.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ?
               new CustomResponse<VerificationCode>(registeredEntity, "Código de Autenticação gerado com Sucesso!") :
               throw new CustomException("Não foi possível gerar o Código de Autenticação. Tente novamente!", HttpStatusCode.InternalServerError);
        }

        private static string GenerateCode()
        {
            var random = new Random();
            const string chars = "1234567890";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
