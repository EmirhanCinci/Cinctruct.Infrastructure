namespace Infrastructure.CrossCuttingConcerns.Exceptions
{
	/// <summary>
	/// Represents an error that occurs when no content is available for a requested resource.
	/// </summary>
	public class NoContentException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NoContentException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public NoContentException(string message) : base(message)
		{

		}
	}
}
