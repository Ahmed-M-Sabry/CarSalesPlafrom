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
    public class EditFuelTypeCommand : IRequest<Result<FuelType>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
