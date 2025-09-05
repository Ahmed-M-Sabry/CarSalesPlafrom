using CarSales.API.ApplicationBase;
using CarSales.Application.Comman;
using CarSales.Application.Features.PostsFeatures.NewCarPostFeature.Commands.Models;
using CarSales.Application.Features.PostsFeatures.OldPost.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.User}")]

    public class NewCarPostController : ApplicationControllerBase
    {
        [HttpPost]
        [Route("Create-New-Cars-Post")]
        public async Task<IActionResult> CreateOldCarPost([FromForm] CreateNewCarPostCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }
        [HttpPut]
        [Route("Edit-Old-Cars-Post")]
        public async Task<IActionResult> EditOldCarPost([FromForm] EditNewCarPostCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }
    }
}
