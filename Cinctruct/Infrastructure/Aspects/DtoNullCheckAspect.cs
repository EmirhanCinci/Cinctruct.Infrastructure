using Castle.DynamicProxy;
using Infrastructure.Constants;
using Infrastructure.CrossCuttingConcerns.Exceptions;
using Infrastructure.Utilities.Interceptors;

namespace Infrastructure.Aspects
{
	/// <summary>
	/// An aspect that checks if the first argument of a method is null and throws a <see cref="BadRequestException"/> if it is.
	/// </summary>
	public class DtoNullCheckAspect : MethodInterception
	{
		/// <summary>
		/// Called before the method execution to check for null arguments.
		/// </summary>
		/// <param name="invocation">The invocation context for the method being intercepted.</param>
		protected override void OnBefore(IInvocation invocation)
		{
			object arg = invocation.Arguments[0];
			if (arg == null)
			{
				throw new BadRequestException(SystemMessages.RequiredModel);
			}
		}
	}
}
