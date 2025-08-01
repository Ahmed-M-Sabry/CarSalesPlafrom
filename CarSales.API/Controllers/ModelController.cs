using CarSales.API.ApplicationBase;
using CarSales.Application.Comman;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Queries.Models;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.ModelFeatures.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = ApplicationRoles.Admin)]
    public class ModelController : ApplicationControllerBase
    {
        [HttpPost("Create-Model")]
        public async Task<IActionResult> CreateModel([FromForm] CreateModelCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }

        [HttpPut("Edit-Model")]
        public async Task<IActionResult> EditModel([FromForm] EditModelCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }

        [HttpPut("Delete-Model")]
        public async Task<IActionResult> DeleteModel([FromForm] int id)
        {
            var result = await Mediator.Send(new DeleteModelCommand(id));
            return result.ResultStatusCode();
        }

        [HttpPut("Restore-Model")]
        public async Task<IActionResult> RestoreModel([FromForm] int id)
        {
            var result = await Mediator.Send(new RestoreModelCommand(id));
            return result.ResultStatusCode();
        }

        [HttpGet("Get-Model-ById")]
        public async Task<IActionResult> GetModelById([FromQuery] int id)
        {
            var result = await Mediator.Send(new GetModelByIdQuery(id));
            return result.ResultStatusCode();
        }

        [HttpGet("Get-All-Models")]
        public async Task<IActionResult> GetAllModels()
        {
            var result = await Mediator.Send(new GetAllModelsQuery());
            return result.ResultStatusCode();
        }

        [HttpGet("Get-Active-Models")]
        public async Task<IActionResult> GetActiveModels()
        {
            var result = await Mediator.Send(new GetActiveModelsQuery());
            return result.ResultStatusCode();
        }

        [HttpGet("Get-Models-ByBrandId")]
        public async Task<IActionResult> GetModelsByBrandId([FromQuery] int brandId)
        {
            var result = await Mediator.Send(new GetModelsByBrandIdQuery(brandId));
            return result.ResultStatusCode();
        }
        [HttpGet("Get-Pagination-Active-Models")]
        public async Task<IActionResult> GetAllActivePaginationAsync([FromQuery] GetPagedActiveModelQuery query)
        {
            var result = await Mediator.Send(query);
            return result.ResultStatusCode();
        }
    }
}