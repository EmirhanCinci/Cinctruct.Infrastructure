namespace Infrastructure.Constants
{
    public class SystemMessages
    {
        /* Response Messages */
        public static string OperationSuccessful = "Operation Successful!";
        public static string OperationFailed = "Operation Failed!";
        public static string InternalServerError = "An error occurred. Please try again later.";

        /* Validation Messages */
        public static string RequiredModel = "A model must be provided.";
        public static string NotEmptyId = "Id value cannot be empty.";
        public static string IdGreaterThanZero = "Id value must be greater than zero.";
        public static string HarmfulContent = "Harmful content detected!";
        public static string InvalidLengthPassword = "Password length must be greater than zero.";

        /* Invalid Messages */
        public static string InvalidValidationClass = "This is not a validation class.";
        public static string InvalidEmailConfiguration = "Email configuration settings are not defined.";
    }
}
