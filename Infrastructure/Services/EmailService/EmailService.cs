using Domain.Dtos.AccountDtos;
using Domain.Dtos.EmailMessageDto;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;


namespace Infrastructure.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _configuration;

    public EmailService(EmailConfiguration configuration)
    {
        _configuration = configuration;
    }

    
    public void SendEmail(EmailMessageDto message,TextFormat format)
    {
        var emailMessage = CreateEmailMessage(message,format);
        Send(emailMessage);
    }
    
    private MimeMessage CreateEmailMessage(EmailMessageDto message,TextFormat format)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("mail",_configuration.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(format) { Text = message.Content };

        return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(_configuration.SmtpServer, _configuration.Port, true);
            client.AuthenticationMechanisms.Remove("OAUTH2");
            client.Authenticate(_configuration.UserName, _configuration.Password);

            client.Send(mailMessage);
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
    
}