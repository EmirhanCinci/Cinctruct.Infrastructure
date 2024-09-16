using Infrastructure.Model.Dtos.Interfaces;

namespace Infrastructure.Utilities.Email.Dtos
{
	/// <summary>
	/// Contains various DTOs for handling email operations.
	/// </summary>
	public class EmailDto
	{
		/// <summary>
		/// DTO for getting email configuration settings.
		/// </summary>
		public class EmailGetDto : IDto
		{
			/// <summary>
			/// The email address of the sender.
			/// </summary>
			public string SenderMail { get; set; } = string.Empty;

			/// <summary>
			/// The password of the sender's email account.
			/// </summary>
			public string SenderPassword { get; set; } = string.Empty;

			/// <summary>
			/// The port number for email service.
			/// </summary>
			public int Port { get; set; }

			/// <summary>
			/// The host address of the email server.
			/// </summary>
			public string Host { get; set; } = string.Empty;

			/// <summary>
			/// Whether SSL is enabled for email communication.
			/// </summary>
			public bool EnableSsl { get; set; }
		}

		/// <summary>
		/// DTO for posting email data with multiple recipients.
		/// </summary>
		public class EmailPostArrayDto : IDto
		{
			/// <summary>
			/// List of recipient email addresses.
			/// </summary>
			public List<string> ReceiverEmails { get; set; } = new List<string>();

			/// <summary>
			/// The subject of the email.
			/// </summary>
			public string Subject { get; set; } = string.Empty;

			/// <summary>
			/// The body content of the email.
			/// </summary>
			public string Body { get; set; } = string.Empty;
		}

		/// <summary>
		/// DTO for posting email data with a single recipient.
		/// </summary>
		public class EmailPostDto : IDto
		{
			/// <summary>
			/// The recipient's email address.
			/// </summary>
			public string ReceiverEmail { get; set; } = string.Empty;

			/// <summary>
			/// The subject of the email.
			/// </summary>
			public string Subject { get; set; } = string.Empty;

			/// <summary>
			/// The body content of the email.
			/// </summary>
			public string Body { get; set; } = string.Empty;
		}

		/// <summary>
		/// DTO for storing the result of an email sending operation.
		/// </summary>
		public class EmailSendResultDto
		{
			/// <summary>
			/// The recipient email address where the email was sent.
			/// </summary>
			public string Email { get; set; } = string.Empty;

			/// <summary>
			/// Indicates if the email was successfully sent.
			/// </summary>
			public bool IsSuccess { get; set; }

			/// <summary>
			/// The error message in case the email sending failed.
			/// </summary>
			public string ErrorMessage { get; set; } = string.Empty;
		}
	}
}
