using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Queries.Models
{
    public class GetAllActiveFuelTypesQuery : IRequest<Result<IEnumerable<FuelType>>>
    {
    }
}
