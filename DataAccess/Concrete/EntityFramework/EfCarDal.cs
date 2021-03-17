
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarProjectContext>, ICarDal
    {
        public List<CarDetailsDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (CarProjectContext context = new CarProjectContext())
            {
                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join co in context.Colors
                             on c.ColorId equals co.ColorId
                             select new CarDetailsDto
                             {
                                 CarId = c.Id,
                                 BrandName = b.BrandName,
                                 BrandId = b.BrandId,
                                 ColorId = co.ColorId,
                                 ColorName = co.ColorName,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ImagePath = (from a in context.CarImages where a.CarId == c.Id select a.ImagePath).FirstOrDefault()
                             };

                return result.ToList();
            }
        }


    }
}