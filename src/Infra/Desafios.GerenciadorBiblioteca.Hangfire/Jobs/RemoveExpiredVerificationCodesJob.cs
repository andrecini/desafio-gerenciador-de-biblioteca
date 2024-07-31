using Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.RemoveMultipleVerificationCodes;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Hangfire.Jobs
{
    public class RemoveExpiredVerificationCodesJob(IMediator mediator)
    {
        private readonly IMediator _mediator = mediator;

        public async Task Execute()
        {
            var result = _mediator.Send(new RemoveMultipleVerificationCodesCommand());
        }
    }
}
