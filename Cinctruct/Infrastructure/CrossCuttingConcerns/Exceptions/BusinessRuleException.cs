namespace Infrastructure.CrossCuttingConcerns.Exceptions
{
	/// <summary>
	/// Represents an error that occurs when a business rule is violated.
	/// </summary>
	public class BusinessRuleException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessRuleException"/> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public BusinessRuleException(string message) : base(message)
		{

		}
	}
}
