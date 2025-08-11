using CarSales.API.ApplicationBase;
using CarSales.Application.Comman;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.BrandFeatures.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class BrandController : ApplicationControllerBase
    {
        [HttpPost("Create-Brand")]
        public async Task<IActionResult> CreateBrand([FromForm] CreateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }
        [HttpPut("Edit-Brand")]
        public async Task<IActionResult> EditBrand([FromForm] EditBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }

        [HttpPut("Delete-Brand")]
        public async Task<IActionResult> DeleteBrand([FromForm] int id)
        {
            var result = await Mediator.Send(new DeleteBrandCommand(id));
            return result.ResultStatusCode();
        }
        [HttpPut("Restore-Brand")]
        public async Task<IActionResult> RestoreBrand([FromForm] int id)
        {
            var result = await Mediator.Send(new RestoreBrandCommand(id));
            return result.ResultStatusCode();
        }
        [HttpGet("Get-Brand-ById")]
        public async Task<IActionResult> GetBrandById([FromQuery] int id)
        {
            var result = await Mediator.Send(new GetBrandByIdQuery(id));
            return result.ResultStatusCode();
        }

        [HttpGet("Get-All-Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await Mediator.Send(new GetAllBrandsQuery());
            return result.ResultStatusCode();
        }

        [HttpGet("Get-Active-Brands")]
        public async Task<IActionResult> GetActiveBrands()
        {
            var result = await Mediator.Send(new GetActiveBrandsQuery());
            return result.ResultStatusCode();
        }
        [HttpGet("Get-Pagination-Active-Brands")]
        public async Task<IActionResult> GetAllActivePaginationAsync([FromQuery] GetPagedActiveBrandsQuery query)
        {
            var result = await Mediator.Send(query);
            return result.ResultStatusCode();
        }

    }
}
