namespace Infrastructure.CrossCuttingConcerns.Exceptions
{
	/// <summary>
	/// Represents an error that occurs when a request is invalid or cannot be processed due to client error.
	/// </summary>
	public class BadRequestException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BadRequestException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public BadRequestException(string message) : base(message)
		{

		}
	}
}
