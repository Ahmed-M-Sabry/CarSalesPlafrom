using CarSales.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.NewCarImageFeatures.Command.Model
{
    public class DeleteImageFromNewCarPostCommand  : IRequest<Result<bool>>
    {
        public int ImageId { get; set; }

    }
}
