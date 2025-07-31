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
        private readonly IValidator<CreateFuelTypeCommand> _validator;

        public CreateFuelTypeHandler(IFuelTypeService fuelTypeService, IValidator<CreateFuelTypeCommand> validator)
        {
            _fuelTypeService = fuelTypeService;
            _validator = validator;
        }

        public async Task<Result<FuelType>> Handle(CreateFuelTypeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return Result<FuelType>.Failure(string.Join(" | ", errors), ErrorType.BadRequest);
            }

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
