using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models;
using CarSales.Application.Features.NewCarImageFeatures.Queries.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Queries.Validator
{
    public class GetNewImageByIdQueryValidator : AbstractValidator<GetNewImageByIdQurey>
    {
        public GetNewImageByIdQueryValidator()
        {
            RuleFor(x => x.ImageId)
                .GreaterThan(0).WithMessage("ImageId must be greater than 0.");
        }
    }
}
