using Desafios.GerenciadorBiblioteca.Hangfire.Helpers;
using Desafios.GerenciadorBiblioteca.Hangfire.Services.Email;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetOverdueLoansIds;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Hangfire.Jobs
{
    public class OverdueLoanAlarmJob(EmailService emailService, IMediator mediator)
    {
        private readonly EmailService _emailService = emailService;
        private readonly IMediator _mediator = mediator;

        public async Task Execute()
        {
            var response = await _mediator.Send(new GetOverdueLoansIdsQuery());
            var infos = response.Data;

            if (response.Total == 0)
                return;

            foreach (var info in infos)
            {
                var content = EmailTemplateHelper.GetOverdueLoanEmailTemplate(info.Username, info.BookName, info.LoanDate, info.LoanValidity);

                await _emailService.SendEmail(info.Email, "Notificação de atraso de Empréstimo", content);
            }            
        }
    }
}
