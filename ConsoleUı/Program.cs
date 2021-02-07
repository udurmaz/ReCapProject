using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUı
{
    public class Program
    {
        static void Main(string[] args)
        {
            AddMetoduTest();
            //foreach (var car in carManager.GetCarsByBrandId(1))
            //{
            //    Console.WriteLine("Arabanın modeli "+ car.Description + " Arabanın üretim yılı "  + car.ModelYear + "Arabanın günlük ücreti " + car.DailyPrice);
            //}
            //Console.ReadLine();


        }

        private static void AddMetoduTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(new Car { BrandId = 2, ColorId = 3, DailyPrice = 120, Description = "Ford", Id = 3, ModelYear = 2010 });
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }
            Console.ReadLine();
        }
    }
}
