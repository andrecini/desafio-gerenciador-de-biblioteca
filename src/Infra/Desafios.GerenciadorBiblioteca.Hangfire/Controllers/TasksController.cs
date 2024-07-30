using Desafios.GerenciadorBiblioteca.Hangfire.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Hangfire.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController(ILogger<TasksController> logger, OverdueLoanAlarmJob overdueLoanAlarmJob) : ControllerBase
    {
        private readonly ILogger<TasksController> _logger = logger;
        private readonly OverdueLoanAlarmJob _overdueLoanAlarmJob = overdueLoanAlarmJob;

        [HttpGet("email/loans/notification")]
        public IActionResult FireAndForget()
        {
            RecurringJobOptions options = new() { TimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time") };

            RecurringJob.AddOrUpdate("Notifica��o de atraso do empr�stimo", () => _overdueLoanAlarmJob.Execute(), "11 0 * * *", options);

            return Ok();
        }
    }
}
