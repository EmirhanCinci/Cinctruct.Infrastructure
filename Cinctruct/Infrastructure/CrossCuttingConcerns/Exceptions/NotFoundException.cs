namespace Infrastructure.CrossCuttingConcerns.Exceptions
{
	/// <summary>
	/// Represents an error that occurs when a requested resource cannot be found.
	/// </summary>
	public class NotFoundException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public NotFoundException(string message) : base(message)
		{

		}
	}
}
