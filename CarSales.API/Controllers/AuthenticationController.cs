using CarSales.API.ApplicationBase;
using CarSales.Application.Features.AuthenticationFeatures.Command.Model;
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
            return ResultStatusCode(result);
        }


    }

}
