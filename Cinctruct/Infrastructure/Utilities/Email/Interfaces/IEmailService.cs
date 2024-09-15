using Infrastructure.Utilities.Email.Dtos;

namespace Infrastructure.Utilities.Email.Interfaces
{
    public interface IEmailService
    {
        Task<List<EmailDto.EmailSendResultDto>> SendEmailAsync(EmailDto.EmailPostArrayDto dto);
        Task<EmailDto.EmailSendResultDto> SendEmailAsync(EmailDto.EmailPostDto dto);
    }
}
