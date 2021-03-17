using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckImageLimit(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ImagesAdded);
        }

        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetAllImagesByCarId(int CarId)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfDefaultImages(CarId));
        }

        public IDataResult<CarImage> GetById(int id)
        {
            //List yok
            return new SuccessDataResult<CarImage>(_carImageDal.Get(cı => cı.CarId == id));
        }

        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }
        private IResult CheckImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(cı => cı.CarId == carId).Count;
            if (result > 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private List<CarImage> CheckIfDefaultImages(int Id)
        {
            var DefaultPath = AppDomain.CurrentDomain.BaseDirectory + "C:\\Users\\udurm\\Desktop\\img\\skoda.jpg";


            var result = _carImageDal.GetAll(p => p.CarId == Id).Any();
            if (!result)
            {
                return new List<CarImage> { new CarImage { CarId = Id, ImagePath = DefaultPath, Date = DateTime.Now } };
            }
            return _carImageDal.GetAll(p => p.CarId == Id);
        }

    }
}