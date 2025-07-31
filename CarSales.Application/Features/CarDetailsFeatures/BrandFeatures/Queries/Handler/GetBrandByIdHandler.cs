using AutoMapper;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Queries.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Queries.Handler
{
    public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdQuery, Result<Brand>>
    {
        private readonly IBrandService _brandService;

        public GetBrandByIdHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }


        public async Task<Result<Brand>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            // Check if the brand Exists 
            var brand = await _brandService.GetByIdAsync(request.Id);

            if (brand is null)
                return Result<Brand>.Failure($"Brand not found with id {request.Id}", ErrorType.NotFound);

            // check if brand is already deleted
            if (brand.IsDeleted)
                return Result<Brand>.Failure($"Brand with id {request.Id} is already deleted", ErrorType.BadRequest);

            // Return the brand
            return Result<Brand>.Success(brand, "Brand Retrieved Successfully");
        }
    }
}
