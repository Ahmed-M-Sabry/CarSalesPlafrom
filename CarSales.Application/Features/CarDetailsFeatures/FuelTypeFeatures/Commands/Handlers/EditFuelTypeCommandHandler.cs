using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Handlers
{
    public class EditFuelTypeCommandHandler : IRequestHandler<EditFuelTypeCommand, Result<FuelType>>
    {
        private readonly IFuelTypeService _fuelTypeService;

        public EditFuelTypeCommandHandler(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;

        }
        public async Task<Result<FuelType>> Handle(EditFuelTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fuelType = await _fuelTypeService.GetByIdAsync(request.Id);
                if (fuelType == null)
                {
                    return Result<FuelType>.Failure("Fuel type not found", ErrorType.NotFound);
                }

                if (fuelType.IsDeleted)
                    return Result<FuelType>.Failure($"Fuel Type with id {request.Id} is already deleted", ErrorType.BadRequest);

                var nameIsExist = await _fuelTypeService.NameIsExistAsync(request.Name, cancellationToken);
                if (nameIsExist is not null && nameIsExist.Id != request.Id)
                {
                    return Result<FuelType>.Failure($"Fuel type with name {request.Name} already exists || Deleted : {nameIsExist.IsDeleted} With ID {nameIsExist.Id}.", ErrorType.BadRequest);
                }

                fuelType.Name = request.Name;
                await _fuelTypeService.UpdateAsync(fuelType);

                return Result<FuelType>.Success(fuelType, "Fuel type updated successfully");
            }
            catch (Exception ex)
            {
                return Result<FuelType>.Failure($"An error occurred: {ex.Message}", ErrorType.InternalServerError);
            }
        }

    }
}
