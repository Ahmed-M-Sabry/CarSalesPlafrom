using CarSales.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models
{
    public class RestoreBrandCommand : IRequest<Result<bool>>
    {
        public int id { get; set; }
        public RestoreBrandCommand(int Id)
        {
            id = Id;
        }

    }
}
