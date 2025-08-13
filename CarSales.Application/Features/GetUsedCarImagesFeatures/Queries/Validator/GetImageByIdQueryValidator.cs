using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Validator
{
    public class GetImageByIdQueryValidator : AbstractValidator<GetImageByIdQurey>
    {
        public GetImageByIdQueryValidator()
        {
            RuleFor(x => x.ImageId)
                .GreaterThan(0).WithMessage("ImageId must be greater than 0.");
        }
    }
}
