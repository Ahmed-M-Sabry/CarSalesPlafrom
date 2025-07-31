using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models
{
    public class CreateFuelTypeCommand : IRequest<Result<FuelType>>
    {
        public string Name { get; set; }
    }
}
