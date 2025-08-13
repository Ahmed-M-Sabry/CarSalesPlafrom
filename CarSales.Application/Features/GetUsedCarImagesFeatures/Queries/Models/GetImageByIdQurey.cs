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
    public class GetImageByIdQurey : IRequest<Result<UsedCarImage>> 
    { 
        public int ImageId { get; set; }
    }
}
