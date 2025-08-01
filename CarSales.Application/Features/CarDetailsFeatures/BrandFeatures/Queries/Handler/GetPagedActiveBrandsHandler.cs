using CarSales.Application.Comman;
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
    public class GetPagedActiveBrandsHandler : IRequestHandler<GetPagedActiveBrandsQuery, Result<PagedResult<Brand>>>
    {
        private readonly IBrandService _brandService;

        public GetPagedActiveBrandsHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<Result<PagedResult<Brand>>> Handle(GetPagedActiveBrandsQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _brandService.GetAllActivePaginationAsync(request.name, request.PageNumber, request.PageSize);

            if (pagedResult is null || !pagedResult.Data.Any())
                return Result<PagedResult<Brand>>.Failure("No brands found", ErrorType.NotFound);

            return Result<PagedResult<Brand>>.Success(pagedResult, "Brands retrieved successfully");
        }
    }

}
