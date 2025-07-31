using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Handlers
{
    public class RestoreFuelTypeCommandHandler : IRequestHandler<RestoreFuelTypeCommand, Result<FuelType>>
    {
        private readonly IFuelTypeService _fuelTypeService;

        public RestoreFuelTypeCommandHandler(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }

        public async Task<Result<FuelType>> Handle(RestoreFuelTypeCommand request, CancellationToken cancellationToken)
        {
            var fuelType = await _fuelTypeService.GetByIdAsync(request.Id);
            if (fuelType == null)
            {
                return Result<FuelType>.Failure("Fuel type not found", ErrorType.NotFound);
            }
            if (!fuelType.IsDeleted)
                return Result<FuelType>.Failure($"Fuel Type with id {request.Id} is Not deleted", ErrorType.BadRequest);

            var restored = await _fuelTypeService.RestoreAsync(fuelType);
            return Result<FuelType>.Success(restored , "Fuel type restored successfully");
        }
    }

}
