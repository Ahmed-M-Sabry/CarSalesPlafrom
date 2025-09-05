using CarSales.Application.Common;
using CarSales.Domain.Entities.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Models
{
    public class EditNewCarPostCommand : IRequest<Result<NewCarPost>>
    {

    }
}
