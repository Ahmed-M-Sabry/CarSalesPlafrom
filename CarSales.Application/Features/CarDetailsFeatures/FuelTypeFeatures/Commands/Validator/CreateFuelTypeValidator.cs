using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Validator
{
    public class CreateFuelTypeValidator : AbstractValidator<CreateFuelTypeCommand>
    {
        public CreateFuelTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Fuel type name is required.")
                .MinimumLength(2).WithMessage("Fuel type name cannot be less than 2 characters.")
                .MaximumLength(100).WithMessage("Fuel type name cannot exceed 100 characters.");
        }
    }
}
