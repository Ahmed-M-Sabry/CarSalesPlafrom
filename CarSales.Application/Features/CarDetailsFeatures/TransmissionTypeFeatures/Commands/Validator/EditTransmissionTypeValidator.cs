using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Validator
{
    public class EditTransmissionTypeValidator : AbstractValidator<EditTransmissionTypeCommand>
    {
        public EditTransmissionTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Transmission type name is required")
                .MinimumLength(2).WithMessage("Transmission type name must be at least 2 characters");
        }
    }
}
