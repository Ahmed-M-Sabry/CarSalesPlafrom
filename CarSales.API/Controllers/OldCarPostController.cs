using CarSales.API.ApplicationBase;
using CarSales.Application.Comman;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using CarSales.Application.Features.PostsFeatures.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.User}")]
    //[Authorize(Roles = "Admin,User")]
    public class OldCarPostController : ApplicationControllerBase
    {
        [HttpPost]
        [Route("Create-Old-Cars-Post")]
        public async Task<IActionResult> CreateOldCarPost([FromForm] CreateOldCarPostCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }
        [HttpPut]
        [Route("Edit-Old-Cars-Post")]
        public async Task<IActionResult> EditOldCarPost([FromForm] EditOldCarPostCommands command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }


    }
}
