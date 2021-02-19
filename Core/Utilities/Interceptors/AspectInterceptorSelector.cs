using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute> //Classın attributelarını okur
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name) //Metodun attributelarını okur 
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //Loglama alt yapısı olmadığı için FileLogger'a şuanlık ihtiyacımız yok

            return classAttributes.OrderBy(x => x.Priority).ToArray(); //Bunları listeye koyar ama Priorityye göre sıralama yapar
        }
    }
}
