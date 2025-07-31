using CarSales.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models
{
    public class DeleteFuelTypeCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }

        public DeleteFuelTypeCommand(int id)
        {
            Id = id;
        }
    }
}
