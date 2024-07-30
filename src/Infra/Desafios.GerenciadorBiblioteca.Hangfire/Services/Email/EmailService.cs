using SendGrid;
using SendGrid.Helpers.Mail;

namespace Desafios.GerenciadorBiblioteca.Hangfire.Services.Email
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SendGridClient _sendGrid;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            var apiKey = _configuration["SendGrid:ApiKey"];
            _sendGrid = new(apiKey);
        }

        public async Task SendEmail(string toEmail, string subject, string content)
        {
            var from = new EmailAddress(_configuration["SendGrid:SenderEmail"], _configuration["SendGrid:SenderName"]);
            var to = new EmailAddress(toEmail);
            var message = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await _sendGrid.SendEmailAsync(message);
        }
    }
}
