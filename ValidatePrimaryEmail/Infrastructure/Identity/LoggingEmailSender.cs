using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ValidatePrimaryEmail.Infrastructure.Identity;

public class LoggingEmailSender(ILogger<LoggingEmailSender> logger) : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var sb = new StringBuilder();
        sb.Append("To: ");
        sb.AppendLine(email);
        sb.Append("Subject: ");
        sb.AppendLine(subject);
        sb.Append("Body: ");
        sb.AppendLine(htmlMessage);

        logger.LogInformation(sb.ToString());

        return Task.CompletedTask;
    }
}
