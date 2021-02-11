using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Utilities.Results;
using Business.Constants;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        public IResult Add(Car car)
        {
            if (car.Description.Length < 2 && car.DailyPrice == 0)
            {
                return new ErrorResult(Messages.CarNameInvalid);
            }
            else
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
            }
            
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>> (_carDal.GetAll(),Messages.CarListed);
        }


        public IDataResult<Car> GetCarsByBrandId(int brandid)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.BrandId == brandid));
        }

        public IDataResult<Car> GetCarsByColorId(int colorid)
        {
            return new SuccessDataResult<Car> (_carDal.Get(c => c.ColorId == colorid));
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new  SuccessResult(Messages.CarUpdated); // Newlemeyi unutma !!
        }

       
    }
}
