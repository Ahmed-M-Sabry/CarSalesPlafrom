using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Models
{
    public class GetModelsByBrandIdQuery : IRequest<Result<IEnumerable<ModelDto>>>
    {
        public int BrandId { get; set; }

        public GetModelsByBrandIdQuery(int brandId)
        {
            BrandId = brandId;
        }
    }
}
