using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //Üstteki kodun amacı , hem classlarda hemde metodlarda kullanılabilir
    //Birden çok kullanılmaya izin verir (Mesela loglama yapsak hem veritabanı hemde dosyaya loglama yapar) birden çok kullanılabilir
    //Başka bir classa inherited edilmeye izin verir 
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor //DynamicProxy'den gelir
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
