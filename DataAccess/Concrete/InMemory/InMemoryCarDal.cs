using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id = 1 , BrandId = 1 , ColorId = 1 , ModelYear = 2014 , DailyPrice = 500000,Description= "BMW"},
                new Car{Id = 2 , BrandId = 2 , ColorId = 1 , ModelYear = 2015 , DailyPrice = 900000,Description= "Ferrari"},
                new Car{Id = 3 , BrandId = 3 , ColorId = 3 , ModelYear = 2019 , DailyPrice = 200000,Description= "Hyundai"},
                new Car{Id = 4 , BrandId = 4 , ColorId = 4 , ModelYear = 2015 , DailyPrice = 800000,Description= "Porsche"},
                new Car{Id = 5 , BrandId = 5 , ColorId = 2 , ModelYear = 2013 , DailyPrice = 150000,Description= "Ford"},
                new Car{Id = 6 , BrandId = 6 , ColorId = 2 , ModelYear = 2014 , DailyPrice = 100000,Description= "Toyata"},
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);

            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int carId)
        {
            return _cars.Where(c => c.Id == carId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.Id = car.Id;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }
    }
}

