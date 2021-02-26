using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Bu bir attributedir
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //Bana validatorType adında bir Type ver demek
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //Gönderilen validatorType , bir IValidator(abstract validator) değilse;
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil"); //Hata gönder
            }

            _validatorType = validatorType; // Hata yoksa benim gönderdiğim validatorType , _validatorType'dır
        }
        protected override void OnBefore(IInvocation invocation) //MethodInterception içinde OnBefore var o yüzden override yaptık
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //Reflection(Çalışma anında başka bir işi yapmamızı sağlar) yapıyor
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //Çalışma tipini buluyor, hangi Base'e sahip olduğunu buluyor.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // Bulduğu Base sınıfının parametlerini buluyor.
            foreach (var entity in entities) // Bulduklarının hepsini gez 
            {
                ValidationTool.Validate(validator, entity); //Validation tool kullanarak Validate et 
            }
        }
    }
}
