using CarSales.Application.Common;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models;
using CarSales.Application.IServices;
using CarSales.Domain.Entities.CarDetails;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarSales.Application.Features.UsedCarImages.Validator
{
    public class AddImagesToOldCarPostCommandValidator : AbstractValidator<AddImagesToOldCarPostCommand>
    {
        public AddImagesToOldCarPostCommandValidator()
        {
            RuleFor(x => x.OldCarPostId)
                .GreaterThan(0).WithMessage("OldCarPostId must be greater than 0.");
        }
    }
}