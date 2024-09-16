using Castle.DynamicProxy;
using FluentValidation;
using Infrastructure.Constants;
using Infrastructure.CrossCuttingConcerns.Validations;
using Infrastructure.Utilities.Interceptors;

namespace Infrastructure.Aspects
{
	/// <summary>
	/// An aspect that validates the arguments of a method using a specified validator.
	/// </summary>
	public class ValidationAspect : MethodInterception
	{
		private readonly Type _validatorType;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationAspect"/> class with a specified validator type.
		/// </summary>
		/// <param name="validatorType">The type of the validator to use. Must implement <see cref="IValidator"/>.</param>
		/// <exception cref="Exception">Thrown if the validator type does not implement <see cref="IValidator"/>.</exception>
		public ValidationAspect(Type validatorType)
		{
			if (!typeof(IValidator).IsAssignableFrom(validatorType))
			{
				throw new Exception(SystemMessages.InvalidValidationClass);
			}
			_validatorType = validatorType;
		}

		/// <summary>
		/// Called before the method execution to validate the arguments using the specified validator.
		/// </summary>
		/// <param name="invocation">The invocation context for the method being intercepted.</param>
		protected override void OnBefore(IInvocation invocation)
		{
			var validator = (IValidator)Activator.CreateInstance(_validatorType);
			var entityType = _validatorType?.BaseType?.GetGenericArguments()[0];
			var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
			foreach (var entity in entities)
			{
				ValidationTool.Validate(validator, entity);
			}
		}
	}
}
