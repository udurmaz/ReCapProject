using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetCarsByColorId(int colorid);
        IDataResult<Car> GetCarsByBrandId(int brandid);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
    }
}
