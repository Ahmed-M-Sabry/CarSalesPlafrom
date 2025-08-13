using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models
{
    public class AddImagesToOldCarPostCommand : IRequest<Result<List<UsedCarImage>>>
    {
        public int OldCarPostId { get; set; }
        public IFormFileCollection? Images { get; set; }
    }
}
