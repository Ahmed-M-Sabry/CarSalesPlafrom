using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Validator
{
    public class EditModelValidator : AbstractValidator<EditModelCommand>
    {
        public EditModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Model ID must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Model name is required")
                .MinimumLength(2).WithMessage("Model name must be at least 2 characters");

            RuleFor(x => x.BrandId)
                .GreaterThan(0).WithMessage("Brand ID must be greater than 0");
        }
    }
}
