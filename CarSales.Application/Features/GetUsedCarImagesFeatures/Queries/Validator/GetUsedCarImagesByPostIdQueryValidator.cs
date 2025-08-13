using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Validator
{
    public class GetUsedCarImagesByPostIdQueryValidator : AbstractValidator<GetUsedCarImagesByPostIdQuery>
    {
        public GetUsedCarImagesByPostIdQueryValidator()
        {
            RuleFor(x => x.OldCarPostId)
                .GreaterThan(0).WithMessage("Old Car Post Id must be greater than 0.");
        }
    }
}
