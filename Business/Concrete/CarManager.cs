using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Utilities.Results;
using Business.Constants;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;
using Entities.DTOs;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [SecuredOperation("car.add, admin")] //Bu yapıya claim diyoruz
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            ValidationTool.Validate(new CarValidator(), car);
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);

        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            Add(car);
            if (car.DailyPrice < 0)
            {
                throw new Exception("Geçersiz günlük ücret");
            }
            Add(car);
            return null;
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect] //key,value
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            if (DateTime.Now.Hour == 5)
            {
                return new ErrorDataResult<List<CarDetailsDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetails(filter));
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandid)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll((c => c.BrandId == brandid)));
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int colorid)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll((c => c.ColorId == colorid)));
        }
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated); // Newlemeyi unutma !!
        }


    }
}
