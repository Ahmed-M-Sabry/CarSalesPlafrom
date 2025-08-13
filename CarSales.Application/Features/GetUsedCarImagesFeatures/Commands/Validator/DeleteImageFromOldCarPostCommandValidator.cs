using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Validator
{
    public class DeleteImageFromOldCarPostCommandValidator : AbstractValidator<DeleteImageFromOldCarPostCommand>
    {
        public DeleteImageFromOldCarPostCommandValidator()
        {
            RuleFor(x => x.ImageId)
                .GreaterThan(0).WithMessage("ImageId must be greater than 0.");
        }
    }
}
