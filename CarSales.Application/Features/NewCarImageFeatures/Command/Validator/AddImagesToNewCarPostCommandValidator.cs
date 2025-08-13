using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models;
using CarSales.Application.Features.NewCarImageFeatures.Command.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Command.Validator
{
    public class AddImagesToNewCarPostCommandValidator : AbstractValidator<AddImagesToNewCarPostCommand>
    {
        public AddImagesToNewCarPostCommandValidator()
        {
            RuleFor(x => x.NewCarPostId)
                .GreaterThan(0).WithMessage("OldCarPostId must be greater than 0.");
        }
    }
}
