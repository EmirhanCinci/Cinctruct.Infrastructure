using Castle.DynamicProxy;
using Infrastructure.Constants;
using Infrastructure.CrossCuttingConcerns.Exceptions;
using Infrastructure.Utilities.Interceptors;

namespace Infrastructure.Aspects
{
	/// <summary>
	/// An aspect that validates the first argument of a method to ensure it is not null and is greater than zero if it's an ID value.
	/// </summary>
	public class IdCheckAspect : MethodInterception
	{
		/// <summary>
		/// Called before the method execution to validate the ID argument.
		/// </summary>
		/// <param name="invocation">The invocation context for the method being intercepted.</param>
		protected override void OnBefore(IInvocation invocation)
		{
			object arg = invocation.Arguments[0];
			if (arg == null)
			{
				throw new BadRequestException(SystemMessages.NotEmptyId);
			}
			if (arg is long longValue)
			{
				CheckId(longValue);
			}
			else if (arg is int intValue)
			{
				CheckId(intValue);
			}
			else if (arg is short shortValue)
			{
				CheckId(shortValue);
			}
			else if (arg is byte byteValue)
			{
				CheckId(byteValue);
			}
		}

		private void CheckId(long id)
		{
			if (id <= 0)
			{
				throw new BadRequestException(SystemMessages.IdGreaterThanZero);
			}
		}
	}
}
