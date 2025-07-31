using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Validator
{
    public class CreateModelValidator : AbstractValidator<CreateModelCommand>
    {
        public CreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Model name is required")
                .MinimumLength(2).WithMessage("Model name must be at least 2 characters");

            RuleFor(x => x.BrandId)
                .GreaterThan(0).WithMessage("Brand ID must be greater than 0");
        }
    }
}
