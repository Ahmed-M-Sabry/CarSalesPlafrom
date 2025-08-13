using CarSales.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models
{
    public class DeleteImageFromOldCarPostCommand : IRequest<Result<bool>> 
    { 
        public int ImageId { get; set; } 

    }
}