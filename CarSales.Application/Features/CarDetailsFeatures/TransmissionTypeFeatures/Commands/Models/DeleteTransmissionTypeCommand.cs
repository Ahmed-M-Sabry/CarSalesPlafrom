using CarSales.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models
{
    public class DeleteTransmissionTypeCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }

        public DeleteTransmissionTypeCommand(int id)
        {
            Id = id;
        }
    }
}
