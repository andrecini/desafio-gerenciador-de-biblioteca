using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using FluentValidation;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Helpers
{
    public static class ValidatorHelper
    {
        public static void ValidateEntity<T, Command>(Command dto)
            where T : AbstractValidator<Command>, new()
        {
            var validatorInstance = new T();
            var result = validatorInstance.Validate(dto);

            if (!result.IsValid)
                throw new CustomException(
                    result.Errors.Select(x => x.ErrorMessage).ToArray(),
                    HttpStatusCode.UnprocessableEntity);
        }
    }
}
