using CarSales.API.ApplicationBase;
using CarSales.Application.Comman;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = ApplicationRoles.Admin)]
    public class BrandController : ApplicationControllerBase
    {

        [HttpPost("Create-Brand")]
        public async Task<IActionResult> CreateBrand([FromForm] CreateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }
        [HttpPost("Edit-Brand")]
        public async Task<IActionResult> EditBrand([FromForm] EditBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }
    }
}
