using Infrastructure.Constants;

namespace Infrastructure.Utilities.Responses
{
    public class CustomApiResponse<T> where T : class
    {
        public int StatusCode { get; set; }
        public bool IsSuccessful { get; set; } = false;
        public string StatusMessage { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();

        public static CustomApiResponse<T> Success(int statusCode, string? statusMessage = null)
        {
            statusMessage ??= SystemMessages.OperationSuccessful;
            return new CustomApiResponse<T> { StatusCode = statusCode, IsSuccessful = true, StatusMessage = statusMessage };
        }

        public static CustomApiResponse<T> Success(int statusCode, T data, string? statusMessage = null)
        {
            statusMessage ??= SystemMessages.OperationSuccessful;
            return new CustomApiResponse<T> { StatusCode = statusCode, IsSuccessful = true, StatusMessage = statusMessage, Data = data };
        }

        public static CustomApiResponse<T> Fail(int statusCode, string errorMessage, string? statusMessage = null)
        {
            statusMessage ??= SystemMessages.OperationFailed;
            return new CustomApiResponse<T> { StatusCode = statusCode, IsSuccessful = false, StatusMessage = statusMessage, ErrorMessages = new List<string> { errorMessage } };
        }

        public static CustomApiResponse<T> Fail(int statusCode, List<string> errorMessages, string? statusMessage = null)
        {
            statusMessage ??= SystemMessages.OperationFailed;
            return new CustomApiResponse<T> { StatusCode = statusCode, IsSuccessful = false, StatusMessage = statusMessage, ErrorMessages = errorMessages };
        }
    }
}
