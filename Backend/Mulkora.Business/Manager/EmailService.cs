using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Mulkora.Business.Abstract;

namespace Mulkora.WebApi.Services;

public class MailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        var senderMail = _configuration["EmailSettings:Sender"];

        var senderPassword = _configuration["EmailSettings:Password"];

        var message = new MimeMessage();

        message.From.Add(new MailboxAddress("Mülkora", senderMail));

        message.To.Add(new MailboxAddress("", toEmail));

        message.Subject = subject;

        message.Body = new BodyBuilder
        {
            HtmlBody = htmlBody
        }.ToMessageBody();

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

        await smtpClient.AuthenticateAsync(senderMail, senderPassword);

        await smtpClient.SendAsync(message);

        await smtpClient.DisconnectAsync(true);
    }
}