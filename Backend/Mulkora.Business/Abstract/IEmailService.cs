namespace Mulkora.Business.Abstract;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string htmlBody);
}