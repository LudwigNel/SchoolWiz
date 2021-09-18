using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SchoolWiz.WebApp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<SmartSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public SmartSettings Options { get; } 

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGrid.SendGridKey, subject, message, email);
        }

        public async Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
          
            var emailMessage = MailHelper.CreateSingleEmail(
                new EmailAddress(Options.SendGrid.FromEmailAddress, Options.SendGrid.FromEmailName),
                new EmailAddress(email),
                subject,
                message,
                message);

             await client.SendEmailAsync(emailMessage).ConfigureAwait(false);
        }
    }
}
