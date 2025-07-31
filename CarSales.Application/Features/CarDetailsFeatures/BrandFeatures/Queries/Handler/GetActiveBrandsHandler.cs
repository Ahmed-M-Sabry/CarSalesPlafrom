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
    public class GetActiveBrandsHandler : IRequestHandler<GetActiveBrandsQuery, Result<IEnumerable<Brand>>>
    {
        private readonly IBrandService _brandService;

        public GetActiveBrandsHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<Result<IEnumerable<Brand>>> Handle(GetActiveBrandsQuery request, CancellationToken cancellationToken)
        {
            var allBrands = await _brandService.GetAllActiveAsync();

            if (allBrands is null || !allBrands.Any())
                return Result<IEnumerable<Brand>>.Failure("No brands found", ErrorType.NotFound);

            return Result<IEnumerable<Brand>>.Success(allBrands, "Brands retrieved successfully");
        }
    }
}
