using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUı
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(new Car { BrandId = 1, ColorId = 1, DailyPrice = 100, Description = "BMW", Id = 1, ModelYear = 2014 });
            foreach (var car in carManager.GetCarsByBrandId(1))
            {
                Console.WriteLine("Arabanın modeli "+ car.Description + " Arabanın üretim yılı "  + car.ModelYear + "Arabanın günlük ücreti " + car.DailyPrice);
            }
            Console.ReadLine();
           
        }
    }
}
