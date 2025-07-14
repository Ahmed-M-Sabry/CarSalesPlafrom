using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Features.AuthenticationFeatures.Command.Model
{
    public class AddNewUserCommand : IRequest<string>
    {
        public string FullName { get; set; }
        private string _email;
        private string _password;
        private string _confirmPassword;
        public string Email
        {
            get => _email;
            set => _email = value ?? throw new ArgumentNullException(nameof(Email));
        }

        public string Password
        {
            get => _password;
            set => _password = value ?? throw new ArgumentNullException(nameof(Password));
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = value ?? throw new ArgumentNullException(nameof(ConfirmPassword));
        }
    }
}
