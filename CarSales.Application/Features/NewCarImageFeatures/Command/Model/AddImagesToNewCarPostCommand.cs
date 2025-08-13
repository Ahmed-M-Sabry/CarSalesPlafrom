using CarSales.Application.Common;
using CarSales.Domain.Entities.CarDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Command.Model
{
    public class AddImagesToNewCarPostCommand : IRequest<Result<List<NewCarImage>>>
    {
        public int NewCarPostId { get; set; }
        public IFormFileCollection? Images { get; set; }
    }
}
