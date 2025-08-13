using CarSales.Application.Features.NewCarImageFeatures.Command.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Command.Validator
{
    public class DeleteImageFromNewCarPostCommandValidator : AbstractValidator<DeleteImageFromNewCarPostCommand>
    {
        public DeleteImageFromNewCarPostCommandValidator()
        {
                RuleFor(x => x.ImageId)
                    .GreaterThan(0).WithMessage("ImageId must be greater than 0.");
        }
    }
}
