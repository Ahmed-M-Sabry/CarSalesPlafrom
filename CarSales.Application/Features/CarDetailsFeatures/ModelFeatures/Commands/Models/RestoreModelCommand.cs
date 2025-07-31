using CarSales.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models
{
    public class RestoreModelCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }

        public RestoreModelCommand(int id)
        {
            Id = id;
        }
    }
}
