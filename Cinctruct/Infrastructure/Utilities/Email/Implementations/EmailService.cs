using Infrastructure.Constants;
using Infrastructure.Utilities.Email.Dtos;
using Infrastructure.Utilities.Email.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Infrastructure.Utilities.Email.Implementations
{
	/// <summary>
	/// Service class responsible for sending emails using SMTP.
	/// </summary>
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailService"/> class.
		/// </summary>
		/// <param name="configuration">The configuration object to retrieve email settings.</param>
		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Sends emails to multiple recipients asynchronously using the provided email configuration.
		/// </summary>
		/// <param name="dto">The data transfer object containing email details and recipients.</param>
		/// <returns>A task representing the asynchronous operation. Contains a list of send results for each recipient.</returns>
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

		/// <summary>
		/// Sends an email to a single recipient asynchronously using the provided email configuration.
		/// </summary>
		/// <param name="dto">The data transfer object containing email details and recipient.</param>
		/// <returns>A task representing the asynchronous operation. Contains the send result for the recipient.</returns>
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
