using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Handlers
{
    public class DeleteFuelTypeCommandHandler : IRequestHandler<DeleteFuelTypeCommand, Result<bool>>
    {
        private readonly IFuelTypeService _fuelTypeService;

        public DeleteFuelTypeCommandHandler(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }

        public async Task<Result<bool>> Handle(DeleteFuelTypeCommand request, CancellationToken cancellationToken)
        {
            var fuelType = await _fuelTypeService.GetByIdAsync(request.Id);
            if (fuelType == null)
            {
                return Result<bool>.Failure("Fuel type not found", ErrorType.NotFound);
            }

            if (fuelType.IsDeleted)
                return Result<bool>.Failure($"Fuel Type with id {request.Id} is already deleted", ErrorType.BadRequest);


            await _fuelTypeService.DeleteAsync(fuelType);
            return Result<bool>.Success(true ,  "Fuel type deleted successfully");
        }
    }

}
