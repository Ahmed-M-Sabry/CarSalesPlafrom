using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Queries.Model
{
    public class GetNewCarImagesByPostIdQuery : IRequest<Result<List<NewCarImage>>>
    {
        public int NewCarPostId { get; set; }
        public GetNewCarImagesByPostIdQuery(int CarPostId)
        {
            NewCarPostId = CarPostId;
        }
    }
}
