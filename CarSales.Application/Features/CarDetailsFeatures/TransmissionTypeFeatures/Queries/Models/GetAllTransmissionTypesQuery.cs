using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Queries.Models
{
    public class GetAllTransmissionTypesQuery : IRequest<Result<IEnumerable<TransmissionType>>>
    {
    }
}
