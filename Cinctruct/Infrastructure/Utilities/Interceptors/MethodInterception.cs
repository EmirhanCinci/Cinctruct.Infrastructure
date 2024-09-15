using Castle.DynamicProxy;

namespace Infrastructure.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation)
        {

        }

        protected virtual void OnAfter(IInvocation invocation)
        {

        }

        protected virtual void OnException(IInvocation invocation, Exception e)
        {

        }

        protected virtual void OnSuccess(IInvocation invocation)
        {

        }

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
