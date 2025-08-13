using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models
{
    public class GetUsedCarImagesByPostIdQuery : IRequest<Result<List<UsedCarImage>>>
    {
        public int OldCarPostId { get; set; }
        public GetUsedCarImagesByPostIdQuery(int CarPostId)
        {
            OldCarPostId = CarPostId;
        }
    }
}
