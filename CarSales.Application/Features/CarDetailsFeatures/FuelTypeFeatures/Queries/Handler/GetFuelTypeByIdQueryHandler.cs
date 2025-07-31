using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Queries.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Queries.Handler
{
    public class GetFuelTypeByIdQueryHandler : IRequestHandler<GetFuelTypeByIdQuery, Result<FuelType>>
    {
        private readonly IFuelTypeService _fuelTypeService;

        public GetFuelTypeByIdQueryHandler(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }

        public async Task<Result<FuelType>> Handle(GetFuelTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var fuelType = await _fuelTypeService.GetByIdAsync(request.Id);
            if (fuelType == null)
            {
                return Result<FuelType>.Failure("Fuel type not found", ErrorType.NotFound);
            }

            if (fuelType.IsDeleted)
                return Result<FuelType>.Failure($"Fuel Type with id {request.Id} is already deleted", ErrorType.BadRequest);


            return Result<FuelType>.Success(fuelType);
        }
    }

}
