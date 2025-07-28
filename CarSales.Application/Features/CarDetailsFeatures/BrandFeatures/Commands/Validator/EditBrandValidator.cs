using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Validator
{
    public class EditBrandValidator : AbstractValidator<EditBrandCommand>
    {
        public EditBrandValidator()
        {
            

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Brand name is Required")
                .MinimumLength(2).WithMessage("Brand name must be at least 2 Characters");
        }
    }
}
