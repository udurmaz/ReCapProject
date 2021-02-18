using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module //Modül olduğunu belirtmemiz gerekiyor (Autofac)
    {
        protected override void Load(ContainerBuilder builder) //Yükleme demek
        {
            //Kodun tanımı = birisi senden IBrandService isterse ona BrandManager ver demek
            //En sonuna .SingleInstance() demeyi unutma
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance(); //Services.Addsingleton'a karşılık geliyor
            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
            builder.RegisterType<RentalManager>().As<IRentalService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

            //Aynı işlemleri EntityFramework katmanları için yapalım
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
            builder.RegisterType<EfCarDal>().As<ICarDal>().SingleInstance();
            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

        }
    }
}
