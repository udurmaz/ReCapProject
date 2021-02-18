using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator:AbstractValidator<Brand> //AbstractValidator hep ekliyoruz Kimin için olacağınıda liste ile ekliyoruz
    {
        //Kurallar bir constructorın içine yazılır 
        // Sonradan kural eklemek istiyorsam .Must ile yapılabilir 
        public BrandValidator()
        {
            RuleFor(b => b.BrandId).NotEmpty();
            RuleFor(b => b.BrandName).NotEmpty();
        }
    }
}
