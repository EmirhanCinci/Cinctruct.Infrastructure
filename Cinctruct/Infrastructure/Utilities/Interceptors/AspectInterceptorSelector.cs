using Castle.DynamicProxy;
using System.Reflection;

namespace Infrastructure.Utilities.Interceptors
{
	/// <summary>
	/// Selects interceptors for a method or class based on attributes and priority.
	/// </summary>
	public class AspectInterceptorSelector : IInterceptorSelector
	{
		/// <summary>
		/// Selects the interceptors to be applied to a given method or class.
		/// </summary>
		/// <param name="type">The type of the class where the method is defined.</param>
		/// <param name="method">The method for which interceptors are being selected.</param>
		/// <param name="interceptors">The array of available interceptors.</param>
		/// <returns>An array of interceptors to be applied to the method.</returns>
		public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
		{
			var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
			var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
			classAttributes.AddRange(methodAttributes);
			return classAttributes.OrderBy(x => x.Priority).ToArray();
		}
	}
}
