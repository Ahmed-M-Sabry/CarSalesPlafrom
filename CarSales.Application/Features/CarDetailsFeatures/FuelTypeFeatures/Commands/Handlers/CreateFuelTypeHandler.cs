using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Handlers
{
    public class CreateFuelTypeHandler : IRequestHandler<CreateFuelTypeCommand, Result<FuelType>>
    {
        private readonly IFuelTypeService _fuelTypeService;

        public CreateFuelTypeHandler(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }

        public async Task<Result<FuelType>> Handle(CreateFuelTypeCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _fuelTypeService.NameIsExistAsync(request.Name, cancellationToken);
            if (isExist != null)
            {
                return Result<FuelType>.Failure("Fuel type name already exists.", ErrorType.Conflict);
            }

            var fuelType = new FuelType
            {
                Name = request.Name
            };

            await _fuelTypeService.CreateAsync(fuelType);

            return Result<FuelType>.Success(fuelType);
        }
    }
}
