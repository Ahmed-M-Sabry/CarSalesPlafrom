using CarSales.API.ApplicationBase;
using CarSales.Application.Comman;
using CarSales.Application.Common;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Commands.Models;
using CarSales.Application.Features.CarDetailsFeatures.TransmissionTypeFeatures.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class TransmissionTypeController : ApplicationControllerBase
    {
        [HttpPost("Create-TransmissionType")]
        public async Task<IActionResult> CreateTransmissionType([FromForm] CreateTransmissionTypeCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }

        [HttpPut("Edit-TransmissionType")]
        public async Task<IActionResult> EditTransmissionType([FromForm] EditTransmissionTypeCommand command)
        {
            var result = await Mediator.Send(command);
            return result.ResultStatusCode();
        }

        [HttpPut("Delete-TransmissionType")]
        public async Task<IActionResult> DeleteTransmissionType([FromForm] int id)
        {
            var result = await Mediator.Send(new DeleteTransmissionTypeCommand(id));
            return result.ResultStatusCode();
        }

        [HttpPut("Restore-TransmissionType")]
        public async Task<IActionResult> RestoreTransmissionType([FromForm] int id)
        {
            var result = await Mediator.Send(new RestoreTransmissionTypeCommand(id));
            return result.ResultStatusCode();
        }

        [HttpGet("Get-TransmissionType-ById")]
        public async Task<IActionResult> GetTransmissionTypeById([FromQuery] int id)
        {
            var result = await Mediator.Send(new GetTransmissionTypeByIdQuery(id));
            return result.ResultStatusCode();
        }

        [HttpGet("Get-All-TransmissionTypes")]
        public async Task<IActionResult> GetAllTransmissionTypes()
        {
            var result = await Mediator.Send(new GetAllTransmissionTypesQuery());
            return result.ResultStatusCode();
        }

        [HttpGet("Get-Active-TransmissionTypes")]
        public async Task<IActionResult> GetActiveTransmissionTypes()
        {
            var result = await Mediator.Send(new GetActiveTransmissionTypesQuery());
            return result.ResultStatusCode();
        }
    }
}