namespace Infrastructure.Constants
{
	/// <summary>
	/// Provides a collection of static system messages used throughout the application.
	/// </summary>
	public class SystemMessages
	{
		/// <summary>
		/// Message indicating that the operation was successful.
		/// </summary>
		public static string OperationSuccessful = "Operation Successful!";

		/// <summary>
		/// Message indicating that the operation failed.
		/// </summary>
		public static string OperationFailed = "Operation Failed!";

		/// <summary>
		/// Message indicating that an internal server error occurred.
		/// </summary>
		public static string InternalServerError = "An error occurred. Please try again later.";

		/// <summary>
		/// Message indicating that a model must be provided.
		/// </summary>
		public static string RequiredModel = "A model must be provided.";

		/// <summary>
		/// Message indicating that the Id value cannot be empty.
		/// </summary>
		public static string NotEmptyId = "Id value cannot be empty.";

		/// <summary>
		/// Message indicating that the Id value must be greater than zero.
		/// </summary>
		public static string IdGreaterThanZero = "Id value must be greater than zero.";

		/// <summary>
		/// Message indicating that harmful content was detected.
		/// </summary>
		public static string HarmfulContent = "Harmful content detected!";

		/// <summary>
		/// Message indicating that the password length must be greater than zero.
		/// </summary>
		public static string InvalidLengthPassword = "Password length must be greater than zero.";

		/// <summary>
		/// Message indicating that the provided class is not a validation class.
		/// </summary>
		public static string InvalidValidationClass = "This is not a validation class.";

		/// <summary>
		/// Message indicating that email configuration settings are not defined.
		/// </summary>
		public static string InvalidEmailConfiguration = "Email configuration settings are not defined.";
	}
}
