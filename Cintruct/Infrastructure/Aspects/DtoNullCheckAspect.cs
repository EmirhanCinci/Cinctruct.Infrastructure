using Castle.DynamicProxy;
using Infrastructure.Constants;
using Infrastructure.CrossCuttingConcerns.Exceptions;
using Infrastructure.Utilities.Interceptors;

namespace Infrastructure.Aspects
{
    public class DtoNullCheckAspect : MethodInterception
    {
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
