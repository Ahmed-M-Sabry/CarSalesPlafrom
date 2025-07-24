using CarSales.API.ApplicationBase;
using CarSales.Application.Features.AuthenticationFeatures.LoginUser.Command.Model;
using CarSales.Application.Features.AuthenticationFeatures.RegisterUser.Command.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApplicationControllerBase
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="command">The user registration data.</param>
        /// <returns>An ApiResponse containing the registered user data or errors.</returns>
        /// <response code="201">User created successfully.</response>
        /// <response code="400">Validation errors occurred.</response>
        /// <response code="409">Email already registered.</response>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] AddNewUserCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromForm] UserLogInCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }


    }

}
