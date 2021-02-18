using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator:AbstractValidator<Car>
    {
        public CarValidator()
        {
            //Özel bir Mesaj vermek istersek .WithMessage ile kodu destekleyebiliriz
            RuleFor(c => c.Description).MinimumLength(2);
            RuleFor(c => c.DailyPrice).NotEqual(0);
            RuleFor(c => c.Id).NotNull();
        }
    }
}
