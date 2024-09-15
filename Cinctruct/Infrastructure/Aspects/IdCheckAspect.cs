using Castle.DynamicProxy;
using Infrastructure.Constants;
using Infrastructure.CrossCuttingConcerns.Exceptions;
using Infrastructure.Utilities.Interceptors;

namespace Infrastructure.Aspects
{
    public class IdCheckAspect : MethodInterception
    {
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
