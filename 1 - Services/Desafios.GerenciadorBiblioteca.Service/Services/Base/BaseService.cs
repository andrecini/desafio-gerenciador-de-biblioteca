using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using FluentValidation;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Base
{
    public class BaseService
    {
        protected void ValidateEntity<T, TDto>(TDto entity)
            where T : AbstractValidator<TDto>, new()
        {
            var validatorInstance = new T();
            var result = validatorInstance.Validate(entity);

            if (!result.IsValid)
                throw new CustomException(
                    result.Errors.Select(x => x.ErrorMessage).ToArray(),
                    HttpStatusCode.UnprocessableEntity);
        }
    }
}
