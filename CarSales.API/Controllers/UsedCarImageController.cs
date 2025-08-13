using CarSales.API.ApplicationBase;
using CarSales.Application.Common;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Commands.Models;
using CarSales.Application.Features.GetUsedCarImagesFeatures.Queries.Models;
using CarSales.Domain.Entities.CarDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsedCarImageController : ApplicationControllerBase
    {

        [HttpGet("Get-Images-By-Id")]
        public async Task<IActionResult> GetImagesByPostId([FromQuery] GetImageByIdQurey qurey)
        {
            var result = await Mediator.Send(qurey);
            return result.ResultStatusCode();
        }
        [HttpGet("Get-Images-By-Post-Id")]
        public async Task<IActionResult> GetImagesByPostId([FromQuery] int id)
        {
            var result = await Mediator.Send(new GetUsedCarImagesByPostIdQuery(id));
            return result.ResultStatusCode();
        }
        [HttpPost("Add-Images-To-Exist-Post")]
        public async Task<IActionResult> AddImagesToExistPost([FromForm] AddImagesToOldCarPostCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }
        [HttpDelete("Delete-Image-By-Id")]
        public async Task<IActionResult> DeleteImage([FromForm] DeleteImageFromOldCarPostCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }
    }
}
