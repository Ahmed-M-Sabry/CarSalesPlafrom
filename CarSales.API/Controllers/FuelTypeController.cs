using CarSales.API.ApplicationBase;
using CarSales.Application.Comman;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.FuelTypeFeatures.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class FuelTypeController :  ApplicationControllerBase
    {

        [HttpPost("Create-FuelType")]
        public async Task<IActionResult> CreateFuelType([FromForm] CreateFuelTypeCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }

        [HttpPut("Edit-FuelType")]
        public async Task<IActionResult> EditFuelType([FromForm] EditFuelTypeCommand command)
        {
            Console.WriteLine($"Incoming Edit: ID = {command.Id}, Name = {command.Name}");
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }

        [HttpPut("Delete-FuelType")]
        public async Task<IActionResult> DeleteFuelType([FromForm] int id)
        {
            var result = await Mediator.Send(new DeleteFuelTypeCommand(id));
            return result.ResultStatusCode();
        }

        [HttpPut("Restore-FuelType")]
        public async Task<IActionResult> RestoreFuelType([FromForm] int id)
        {
            var result = await Mediator.Send(new RestoreFuelTypeCommand(id));
            return result.ResultStatusCode();
        }

        [HttpGet("Get-FuelType-ById")]
        public async Task<IActionResult> GetFuelTypeById([FromQuery] int id)
        {
            var result = await Mediator.Send(new GetFuelTypeByIdQuery(id));
            return result.ResultStatusCode();
        }

        [HttpGet("Get-All-FuelTypes")]
        public async Task<IActionResult> GetAllFuelTypes()
        {
            var result = await Mediator.Send(new GetAllFuelTypesQuery());
            return result.ResultStatusCode();
        }

        [HttpGet("Get-Active-FuelTypes")]
        public async Task<IActionResult> GetAllActiveFuelTypes()
        {
            var result = await Mediator.Send(new GetAllActiveFuelTypesQuery());
            return result.ResultStatusCode();
        }
    }

}
