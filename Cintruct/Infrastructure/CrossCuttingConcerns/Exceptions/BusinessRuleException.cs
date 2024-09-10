namespace Infrastructure.CrossCuttingConcerns.Exceptions
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {

        }
    }
}
