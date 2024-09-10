using Infrastructure.Utilities.Email.Dtos;
using Infrastructure.Utilities.Email.Interfaces;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Infrastructure.Constants;

namespace Infrastructure.Utilities.Email.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<EmailDto.EmailSendResultDto>> SendEmailAsync(EmailDto.EmailPostArrayDto dto)
        {
            var emailConfig = _configuration.GetSection("EmailConfiguration").Get<EmailDto.EmailGetDto>();
            var results = new List<EmailDto.EmailSendResultDto>();
            if (emailConfig == null)
            {
                throw new InvalidOperationException(SystemMessages.InvalidEmailConfiguration);
            }
            foreach (var receiverEmail in dto.ReceiverEmails)
            {
                var result = new EmailDto.EmailSendResultDto { Email = receiverEmail };
                try
                {
                    using SmtpClient client = new SmtpClient(emailConfig.Host)
                    {
                        Port = emailConfig.Port,
                        EnableSsl = emailConfig.EnableSsl,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(emailConfig.SenderMail, emailConfig.SenderPassword)
                    };
                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(emailConfig.SenderMail),
                        Subject = dto.Subject,
                        SubjectEncoding = Encoding.UTF8,
                        Body = dto.Body,
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8
                    };
                    mail.To.Add(receiverEmail);
                    await client.SendMailAsync(mail);
                    result.IsSuccess = true;
                }
                catch (Exception exception)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = exception.ToString();
                }
                results.Add(result);
            }
            return results;
        }

        public async Task<EmailDto.EmailSendResultDto> SendEmailAsync(EmailDto.EmailPostDto dto)
        {
            var emailConfig = _configuration.GetSection("EmailConfiguration").Get<EmailDto.EmailGetDto>();
            if (emailConfig == null)
            {
                throw new InvalidOperationException(SystemMessages.InvalidEmailConfiguration);
            }
            var result = new EmailDto.EmailSendResultDto { Email = dto.ReceiverEmail };
            try
            {
                using SmtpClient client = new SmtpClient(emailConfig.Host)
                {
                    Port = emailConfig.Port,
                    EnableSsl = emailConfig.EnableSsl,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailConfig.SenderMail, emailConfig.SenderPassword)
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(emailConfig.SenderMail),
                    Subject = dto.Subject,
                    SubjectEncoding = Encoding.UTF8,
                    Body = dto.Body,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8
                };

                mail.To.Add(dto.ReceiverEmail);
                await client.SendMailAsync(mail);
                result.IsSuccess = true;
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                result.ErrorMessage = exception.ToString();
            }
            return result;
        }
    }
}
