using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constants;


namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); //Metni "," ile ayırıp , Array'e atıyor
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); //Autofac'de yaptığımız injection değerlerini alıyor. , DependencyInjection paketinden geliyor

        }

        protected override void OnBefore(IInvocation invocation) //Metodun önünde çalıştır demek
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); //O anda rollerini bul
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; //İlgili rol varsa return et
                }
            }
            throw new Exception(Messages.AuthorizationDenied); //Yoksa hata mesajı gönder
        }
    }
}