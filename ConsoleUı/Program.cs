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
            //AddMetoduTest();
            //foreach (var car in carManager.GetCarsByBrandId(1))
            //{
            //    Console.WriteLine("Arabanın modeli "+ car.Description + " Arabanın üretim yılı "  + car.ModelYear + "Arabanın günlük ücreti " + car.DailyPrice);
            //}
            //Console.ReadLine();

            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            GetCustomersDetailTest();
            void GetCustomersDetailTest()
            {
                CustomerManager customerManager = new CustomerManager(new EfCustomerDal());

                var result = customerManager.GetAll();

                if (result.Success)
                {
                    foreach (var customer in result.Data)
                    {
                        Console.WriteLine("{0} {1}", customer.CompanyName,
                            customer.UserId, customer.CompanyName);
                    }
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
            }


            //var result = rentalManager.GetRentalDetails();
            //foreach (var rental in result.Data)
            //{
            //    Console.WriteLine(rental.FirstName + " / " + rental.LastName);
            //}


            //AddMetodu2(carManager);

        }

        private static void AddMetodu2(CarManager carManager)
        {
            carManager.Add(new Car
            {
                Id = 8,
                BrandId = 3,
                ColorId = 10,
                DailyPrice = 200,
                ModelYear = 2006,
                Description = "Tır"
            });
        }

        //private static void AddMetoduTest()
        //{
        //    CarManager carManager = new CarManager(new EfCarDal());
        //    carManager.Add(new Car { BrandId = 2, ColorId = 3, DailyPrice = 120, Description = "Ford", Id = 3, ModelYear = 2010 });
        //    foreach (var car in carManager.GetAll())
        //    {
        //        Console.WriteLine(car.Description);
        //    }
        //    Console.ReadLine();
        //}
    }
}
