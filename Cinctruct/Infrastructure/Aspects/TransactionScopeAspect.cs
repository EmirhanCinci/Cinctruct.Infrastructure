using Castle.DynamicProxy;
using Infrastructure.Utilities.Interceptors;
using System.Transactions;

namespace Infrastructure.Aspects
{
	/// <summary>
	/// AOP TransactionScopeAspect class to handle transaction management using the TransactionScope.
	/// Automatically manages transactions, ensuring that changes are committed or rolled back depending on success or failure.
	/// </summary>
	public class TransactionScopeAspect : MethodInterception
	{
		/// <summary>
		/// Intercepts the method execution to apply transaction management logic.
		/// </summary>
		/// <param name="invocation">Method invocation information.</param>
		public override void Intercept(IInvocation invocation)
		{
			using (TransactionScope transactionScope = new TransactionScope())
			{
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
					transactionScope.Complete();
				}
				catch (Exception)
				{
					transactionScope.Dispose();
					throw;
				}
			}
		}
	}
}
