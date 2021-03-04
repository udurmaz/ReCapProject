using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60) //Cache'de 60 dakika duracak
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //Hangi cache manager kullanacağımı belirtttim
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //ReflectedType : namespace + (hangi)managersa onu  al demek , sonrasında metodu al
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; //key oluşturduk
            if (_cacheManager.IsAdd(key)) // Önceden var mı ?
            {
                invocation.ReturnValue = _cacheManager.Get(key); //metedo çalıştırmadan geri dön
                return;
            }
            invocation.Proceed(); //yoksa metodu devam ettir
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //cacheimize ekliyoruz
        }
    }
}
