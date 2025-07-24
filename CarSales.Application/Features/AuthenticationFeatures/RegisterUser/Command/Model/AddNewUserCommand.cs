using CarSales.Application.Comman;
using CarSales.Application.Common;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Model
{
    public class AddNewUserCommand : IRequest<Result<RegisterUserDto>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
