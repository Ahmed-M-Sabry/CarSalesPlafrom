using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Handler
{
    public class RestoreBrandHandler : IRequestHandler<RestoreBrandCommand, Result<bool>>
    {
        private readonly IBrandService _brandService;

        public RestoreBrandHandler(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<Result<bool>> Handle(RestoreBrandCommand request, CancellationToken cancellationToken)
        {
            // Check if the brand Exists
            var brand = await _brandService.GetByIdAsync(request.id);

            if (brand is null)
                return Result<bool>.Failure($"Brand not found with id {request.id}", ErrorType.NotFound);

            // check if brand is already deleted
            if (!brand.IsDeleted)
                return Result<bool>.Failure($"Brand with id {request.id} is Not deleted", ErrorType.BadRequest);

            // Delete the brand 
            await _brandService.RestoreAsync(brand);

            // Return Success Result
            return Result<bool>.Success(true, "Brand Restored successfully");
        }
    }
}
