using CarSales.API.ApplicationBase;
using CarSales.Application.Features.AuthenticationFeatures.LoginUser.Command.Model;
using CarSales.Application.Features.AuthenticationFeatures.Logout.Command;
using CarSales.Application.Features.AuthenticationFeatures.RefreshToken.Model;
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
            if (result.IsSuccess && result.Value?.RefreshToken != null && result.Value.CookieOptions != null)
            {
                Response.Cookies.Append("RefreshToken", result.Value.RefreshToken, result.Value.CookieOptions);
            }
            return result.ResultStatusCode();
        }

        [HttpPost("Generate-New-token-From-RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await Mediator.Send(new RefreshTokenCommand());

            if (result.IsSuccess && result.Value?.RefreshToken != null && result.Value.CookieOptions != null)
            {
                Response.Cookies.Delete("RefreshToken");
                Response.Cookies.Append("RefreshToken", result.Value.RefreshToken, result.Value.CookieOptions);
            }
            return result.ResultStatusCode();
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await Mediator.Send(new UserLogoutCommand());
            
            return result.ResultStatusCode();
        }

        // To Do
        // 2- Add EndPoint to Get Token To Rest Password

        // 3- Add Endpoint To Change Password From Gmail 



    }

}
