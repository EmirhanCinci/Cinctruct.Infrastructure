using FluentValidation;

namespace Infrastructure.CrossCuttingConcerns.Validations
{
	/// <summary>
	/// Provides validation utilities to validate entities using a specified validator.
	/// </summary>
	public static class ValidationTool
	{
		/// <summary>
		/// Validates the specified entity using the provided validator.
		/// </summary>
		/// <param name="validator">The validator used to validate the entity.</param>
		/// <param name="entity">The entity to validate.</param>
		/// <exception cref="ValidationException">Thrown when validation fails.</exception>
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
