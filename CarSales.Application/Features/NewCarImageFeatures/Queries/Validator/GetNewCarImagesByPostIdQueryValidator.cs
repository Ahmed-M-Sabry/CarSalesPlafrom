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
    public class GetNewCarImagesByPostIdQueryValidator : AbstractValidator<GetNewCarImagesByPostIdQuery>
    {
        public GetNewCarImagesByPostIdQueryValidator()
        {
            RuleFor(x => x.NewCarPostId)
                .GreaterThan(0).WithMessage("ImageId must be greater than 0.");
        }
    }
}
