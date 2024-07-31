using Desafios.GerenciadorBiblioteca.Hangfire.Helpers;
using Desafios.GerenciadorBiblioteca.Hangfire.Services.Email;
using Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.GenerateVerificationCode;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Hangfire.Jobs
{
    public class SendVerificationCodeJob(IMediator mediator, EmailService emailService)
    {
        private readonly EmailService _emailService = emailService;
        private readonly IMediator _mediator = mediator;

        public async Task Execute(int userId, string userEmail)
        {
            var result = await _mediator.Send(new GenerateVerificationCodeCommand(userId));

            var content = EmailTemplateHelper.GetVerificationEmailTemplate(result.Data.Code);

            await _emailService.SendEmail(userEmail, "Código de confirmação de Acesso", content);
        }
    }
}
