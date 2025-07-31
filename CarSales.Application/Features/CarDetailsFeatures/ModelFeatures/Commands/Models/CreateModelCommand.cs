using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Dtos;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models
{
    public class CreateModelCommand : IRequest<Result<ModelDto>>
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
    }
}
