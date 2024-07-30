using Desafios.GerenciadorBiblioteca.Hangfire.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Hangfire.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController(
        ILogger<TasksController> logger,
        OverdueLoanAlarmJob overdueLoanAlarmJob,
        RemoveExpiredVerificationCodesJob removeExpiredVerificationCodesJob,
        SendVerificationCodeJob sendVerificationCodeJob) : ControllerBase
    {
        private readonly ILogger<TasksController> _logger = logger;
        private readonly OverdueLoanAlarmJob _overdueLoanAlarmJob = overdueLoanAlarmJob;
        private readonly RemoveExpiredVerificationCodesJob _removeExpiredVerificationCodesJob = removeExpiredVerificationCodesJob;
        private readonly SendVerificationCodeJob _sendVerificationCodeJob = sendVerificationCodeJob;

        [HttpGet("email/loans/notification")]
        public IActionResult LoanOverdueNotification()
        {
            RecurringJobOptions options = new() { TimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time") };

            RecurringJob.AddOrUpdate("Notificação de atraso do empréstimo", () => _overdueLoanAlarmJob.Execute(), "11 0 * * *", options);

            return Ok();
        }

        [HttpGet("verification-codes/cleaning")]
        public IActionResult RemoveExpiredVerificationCodes()
        {
            RecurringJobOptions options = new() { TimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time") };

            RecurringJob.AddOrUpdate("Limpeza dos Códigos de Verificação expirados", () => _removeExpiredVerificationCodesJob.Execute(), Cron.Weekly, options);

            return Ok();
        }

        [HttpGet("verification-codes/generate")]
        public IActionResult RemoveExpiredVerificationCodes(int userId, string userEmail)
        {
            BackgroundJob.Enqueue(() => _sendVerificationCodeJob.Execute(userId, userEmail));

            return Ok();
        }
    }
}
