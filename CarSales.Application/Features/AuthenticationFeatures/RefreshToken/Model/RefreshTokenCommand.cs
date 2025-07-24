using CarSales.Application.Common;
using CarSales.Domain.AuthenticationHepler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.AuthenticationFeatures.RefreshToken.Model
{
    public class RefreshTokenCommand : IRequest<Result<ResponseAuthModel>>
    {

    }
}
