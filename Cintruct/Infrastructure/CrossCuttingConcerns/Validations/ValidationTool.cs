using FluentValidation;

namespace Infrastructure.CrossCuttingConcerns.Validations
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                var message = string.Join(" - ", result.Errors.Select(error => $"{error.ErrorMessage}"));
                throw new ValidationException(message);
            }
        }
    }
}
