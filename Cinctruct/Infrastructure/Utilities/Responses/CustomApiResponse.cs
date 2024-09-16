using Infrastructure.Constants;

namespace Infrastructure.Utilities.Responses
{
	/// <summary>
	/// Represents a standardized API response with status code, success flag, status message, data, and error messages.
	/// </summary>
	/// <typeparam name="T">The type of the data contained in the response.</typeparam>
	public class CustomApiResponse<T> where T : class
	{
		/// <summary>
		/// Gets or sets the HTTP status code for the response.
		/// </summary>
		public int StatusCode { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the operation was successful.
		/// </summary>
		public bool IsSuccessful { get; set; } = false;

		/// <summary>
		/// Gets or sets the status message providing additional information about the operation.
		/// </summary>
		public string StatusMessage { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the data returned with the response.
		/// </summary>
		public T? Data { get; set; }

		/// <summary>
		/// Gets or sets the list of error messages if the operation failed.
		/// </summary>
		public List<string> ErrorMessages { get; set; } = new List<string>();

		/// <summary>
		/// Creates a successful API response with a status code and optional status message.
		/// </summary>
		/// <param name="statusCode">The HTTP status code to set for the response.</param>
		/// <param name="statusMessage">The optional status message providing additional information.</param>
		/// <returns>A successful <see cref="CustomApiResponse{T}"/> instance.</returns>
		public static CustomApiResponse<T> Success(int statusCode, string? statusMessage = null)
		{
			statusMessage ??= SystemMessages.OperationSuccessful;
			return new CustomApiResponse<T> { StatusCode = statusCode, IsSuccessful = true, StatusMessage = statusMessage };
		}

		/// <summary>
		/// Creates a successful API response with a status code, data, and optional status message.
		/// </summary>
		/// <param name="statusCode">The HTTP status code to set for the response.</param>
		/// <param name="data">The data to include in the response.</param>
		/// <param name="statusMessage">The optional status message providing additional information.</param>
		/// <returns>A successful <see cref="CustomApiResponse{T}"/> instance with data.</returns>
		public static CustomApiResponse<T> Success(int statusCode, T data, string? statusMessage = null)
		{
			statusMessage ??= SystemMessages.OperationSuccessful;
			return new CustomApiResponse<T> { StatusCode = statusCode, IsSuccessful = true, StatusMessage = statusMessage, Data = data };
		}

		/// <summary>
		/// Creates a failed API response with a status code, a single error message, and optional status message.
		/// </summary>
		/// <param name="statusCode">The HTTP status code to set for the response.</param>
		/// <param name="errorMessage">The error message describing the failure.</param>
		/// <param name="statusMessage">The optional status message providing additional information.</param>
		/// <returns>A failed <see cref="CustomApiResponse{T}"/> instance with an error message.</returns>
		public static CustomApiResponse<T> Fail(int statusCode, string errorMessage, string? statusMessage = null)
		{
			statusMessage ??= SystemMessages.OperationFailed;
			return new CustomApiResponse<T> { StatusCode = statusCode, IsSuccessful = false, StatusMessage = statusMessage, ErrorMessages = new List<string> { errorMessage } };
		}

		/// <summary>
		/// Creates a failed API response with a status code, a list of error messages, and optional status message.
		/// </summary>
		/// <param name="statusCode">The HTTP status code to set for the response.</param>
		/// <param name="errorMessages">The list of error messages describing the failures.</param>
		/// <param name="statusMessage">The optional status message providing additional information.</param>
		/// <returns>A failed <see cref="CustomApiResponse{T}"/> instance with error messages.</returns>
		public static CustomApiResponse<T> Fail(int statusCode, List<string> errorMessages, string? statusMessage = null)
		{
			statusMessage ??= SystemMessages.OperationFailed;
			return new CustomApiResponse<T> { StatusCode = statusCode, IsSuccessful = false, StatusMessage = statusMessage, ErrorMessages = errorMessages };
		}
	}
}
