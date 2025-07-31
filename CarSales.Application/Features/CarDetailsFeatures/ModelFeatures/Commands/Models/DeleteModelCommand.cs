using CarSales.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models
{
    public class DeleteModelCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }

        public DeleteModelCommand(int id)
        {
            Id = id;
        }
    }
}
