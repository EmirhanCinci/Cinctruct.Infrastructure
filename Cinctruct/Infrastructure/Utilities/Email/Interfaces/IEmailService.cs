using Infrastructure.Utilities.Email.Dtos;

namespace Infrastructure.Utilities.Email.Interfaces
{
	/// <summary>
	/// Interface that defines email sending services.
	/// </summary>
	public interface IEmailService
	{
		/// <summary>
		/// Sends an email to multiple recipients asynchronously.
		/// </summary>
		/// <param name="dto">DTO containing the email data and recipients.</param>
		/// <returns>A task representing the asynchronous operation. It contains a list of send results for each recipient.</returns>
		Task<List<EmailDto.EmailSendResultDto>> SendEmailAsync(EmailDto.EmailPostArrayDto dto);

		/// <summary>
		/// Sends an email to a single recipient asynchronously.
		/// </summary>
		/// <param name="dto">DTO containing the email data and recipient.</param>
		/// <returns>A task representing the asynchronous operation. It contains the result of the email send operation..</returns>
		Task<EmailDto.EmailSendResultDto> SendEmailAsync(EmailDto.EmailPostDto dto);
	}
}
