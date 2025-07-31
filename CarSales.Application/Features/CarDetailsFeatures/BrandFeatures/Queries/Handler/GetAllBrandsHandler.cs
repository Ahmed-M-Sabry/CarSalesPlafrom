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
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery,Result<IEnumerable<Brand>>>
    {
        private readonly IBrandService _brandService;
        
        public GetAllBrandsHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<Result<IEnumerable<Brand>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var allBrands = await _brandService.GetAllAsync();

            if(allBrands is null || !allBrands.Any())
                return Result<IEnumerable<Brand>>.Failure("No brands found", ErrorType.NotFound);

            return Result < IEnumerable < Brand >> .Success(allBrands, "Brands retrieved successfully");
        }
    }
}
