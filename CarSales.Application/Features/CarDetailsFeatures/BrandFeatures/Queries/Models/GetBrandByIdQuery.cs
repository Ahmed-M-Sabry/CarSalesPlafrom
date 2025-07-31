using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Queries.Models
{
    public class GetBrandByIdQuery : IRequest<Result<Brand>>
    {
        public int Id { get; set; }
        public GetBrandByIdQuery(int id)
        {
            Id = id;
        }
    }

}
