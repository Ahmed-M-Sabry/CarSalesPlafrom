using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models
{
    public class CreateTransmissionTypeCommand : IRequest<Result<TransmissionType>>
    {
        public string Name { get; set; }
    }
}
