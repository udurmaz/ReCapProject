using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        public void Add(Car car)
        {
            if (car.Description.Length < 2 && car.DailyPrice == 0)
            {
                Console.WriteLine("Geçersiz Araba");
            }
            else
            {
                _carDal.Add(car);
            }
            
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }


        public Car GetCarsByBrandId(int brandid)
        {
            return _carDal.Get(c => c.BrandId == brandid);
        }

        public Car GetCarsByColorId(int colorid)
        {
            return _carDal.Get(c => c.ColorId == colorid);
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
        }
    }
}
