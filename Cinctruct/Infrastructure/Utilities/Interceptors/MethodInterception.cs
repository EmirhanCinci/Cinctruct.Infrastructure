using Castle.DynamicProxy;

namespace Infrastructure.Utilities.Interceptors
{
	/// <summary>
	/// Metod kesme için temel bir uygulama sağlar, metod çağrıları sırasında özelleştirilmiş davranışlar tanımlanmasına olanak tanır.
	/// </summary>
	public class MethodInterception : MethodInterceptionBaseAttribute
	{
		/// <summary>
		/// Method called before the actual method execution. Can be overridden to implement custom pre-processing logic.
		/// </summary>
		/// <param name="invocation">The invocation information for the method being intercepted.</param>
		protected virtual void OnBefore(IInvocation invocation)
		{

		}

		/// <summary>
		/// Method called after the actual method execution. Can be overridden to implement custom post-processing logic.
		/// </summary>
		/// <param name="invocation">The invocation information for the method being intercepted.</param>
		protected virtual void OnAfter(IInvocation invocation)
		{

		}

		/// <summary>
		/// Method called when an exception occurs during method execution. Can be overridden to handle exceptions.
		/// </summary>
		/// <param name="invocation">The invocation information for the method being intercepted.</param>
		/// <param name="e">The exception that was thrown during method execution.</param>
		protected virtual void OnException(IInvocation invocation, Exception e)
		{

		}

		/// <summary>
		/// Method called when the method execution is successful. Can be overridden to implement custom success logic.
		/// </summary>
		/// <param name="invocation">The invocation information for the method being intercepted.</param>
		protected virtual void OnSuccess(IInvocation invocation)
		{

		}

		/// <summary>
		/// Intercepts the method call and invokes the appropriate pre, post, and exception handling logic.
		/// </summary>
		/// <param name="invocation">The invocation information for the method being intercepted.</param>
		public override void Intercept(IInvocation invocation)
		{
			var isSuccess = true;
			OnBefore(invocation);
			try
			{
				invocation.Proceed();
				if (invocation.ReturnValue is Task returnValueTask)
				{
					returnValueTask.GetAwaiter().GetResult();
				}
				if (invocation.ReturnValue is Task task && task.Exception != null)
				{
					throw task.Exception;
				}
			}
			catch (Exception exception)
			{
				isSuccess = false;
				OnException(invocation, exception);
				throw;
			}
			finally
			{
				if (isSuccess)
				{
					OnSuccess(invocation);
				}
				OnAfter(invocation);
			}
		}
	}
}
