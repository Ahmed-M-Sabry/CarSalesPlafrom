﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Dtos
{
    public class RegisterUserDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
