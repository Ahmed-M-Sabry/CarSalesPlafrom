using CarSales.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.AuthenticationFeatures.Logout.Command
{
    public class UserLogoutCommand : IRequest<Result<bool>>
    {

    }
}
