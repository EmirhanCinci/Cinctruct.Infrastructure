using Castle.DynamicProxy;

namespace Infrastructure.Utilities.Interceptors
{
	/// <summary>
	/// Base attribute class for method interception, used to define interception behavior for methods or classes.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class MethodInterceptionBaseAttribute : Attribute, IInterceptor
	{
		/// <summary>
		/// Gets or sets the priority of the interceptor. Higher values indicate higher priority.
		/// </summary>
		public int Priority { get; set; }

		/// <summary>
		/// Method to intercept method calls. Can be overridden to implement custom interception logic.
		/// </summary>
		/// <param name="invocation">The invocation information for the method being intercepted.</param>
		public virtual void Intercept(IInvocation invocation)
		{

		}
	}
}
