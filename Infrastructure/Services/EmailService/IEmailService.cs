using Domain.Dtos.EmailMessageDto;
using MimeKit.Text;

namespace Infrastructure.Services.EmailService;

public interface IEmailService
{
    void SendEmail(EmailMessageDto model,TextFormat format);
}