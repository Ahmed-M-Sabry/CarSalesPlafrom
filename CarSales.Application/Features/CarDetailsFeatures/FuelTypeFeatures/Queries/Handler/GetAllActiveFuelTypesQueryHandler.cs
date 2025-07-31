using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Queries.Models;
using CarSales.Application.IServices.CarDetailsServices;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Queries.Handler
{
    public class GetAllActiveFuelTypesQueryHandler : IRequestHandler<GetAllActiveFuelTypesQuery, Result<IEnumerable<FuelType>>>
    {
        private readonly IFuelTypeService _fuelTypeService;

        public GetAllActiveFuelTypesQueryHandler(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }

        public async Task<Result<IEnumerable<FuelType>>> Handle(GetAllActiveFuelTypesQuery request, CancellationToken cancellationToken)
        {
            var fuelTypes = await _fuelTypeService.GetAllActiveAsync();

            if (fuelTypes == null || !fuelTypes.Any())
            {
                return Result<IEnumerable<FuelType>>.Failure("No fuel types found", ErrorType.NotFound);
            }
            return Result<IEnumerable<FuelType>>.Success(fuelTypes);
        }
    }

}
