using CarSales.Application.Features.PostsFeatures.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.Validator
{
    internal class CreateOldCarPostValidator : AbstractValidator<CreateOldCarPostCommand>
    {
        public CreateOldCarPostValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(p => p.BrandId)
                .GreaterThan(0).WithMessage("Brand ID must be greater than 0.");

            RuleFor(p => p.ModelId)
                .GreaterThan(0).WithMessage("Model ID must be greater than 0.");

            RuleFor(p => p.FuelTypeId)
                .GreaterThan(0).WithMessage("Fuel Type ID must be greater than 0.");

            RuleFor(p => p.TransmissionTypeId)
                .GreaterThan(0).WithMessage("Transmission Type ID must be greater than 0.");

            RuleFor(p => p.Year)
                .GreaterThan(1900).WithMessage("Year must be greater than 1900.")
                .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("Year cannot be in the future.");

            RuleFor(p => p.MileageKm)
                .GreaterThanOrEqualTo(0).WithMessage("Mileage cannot be negative.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

            RuleFor(p => p.Images)
                .Must(images => images == null || images.All(img => img.Length <= 5 * 1024 * 1024))
                .WithMessage("Each File must be less than 5MB.");
        }
    }
}
