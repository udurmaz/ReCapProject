using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarProjectContext>, IRentalDal
    {
        public List<RentalDetailsDto> GetRentalDetails()
        {
            using (CarProjectContext context = new CarProjectContext())
            {
                //Linq kütüphanesini eklemeyi unutma 
                var result = from c in context.Customers
                             join u in context.Users
                             on c.UserId equals u.userId
                             select new RentalDetailsDto
                             {
                                 UserId = c.UserId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 CompanyName = c.CompanyName

                             };
                return result.ToList();




            }
        }
        
    }
}
