using Castle.DynamicProxy;
using FluentValidation;
using Infrastructure.Constants;
using Infrastructure.CrossCuttingConcerns.Validations;
using Infrastructure.Utilities.Interceptors;

namespace Infrastructure.Aspects
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(SystemMessages.InvalidValidationClass);
            }
            _validatorType = validatorType;
        }

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
