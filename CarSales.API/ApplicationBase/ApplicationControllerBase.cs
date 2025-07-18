using CarSales.Application.Comman;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.ApplicationBase
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationControllerBase : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;

        public ObjectResult ResultStatusCode<T>(ApiResponse<T> response)
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(response);
                case System.Net.HttpStatusCode.Created:
                    return Created("", response);
                case System.Net.HttpStatusCode.Unauthorized:
                    return Unauthorized(response);
                case System.Net.HttpStatusCode.NotFound:
                    return NotFound(response);
                case System.Net.HttpStatusCode.BadRequest:
                    return BadRequest(response);
                case System.Net.HttpStatusCode.UnprocessableEntity:
                    return UnprocessableEntity(response);
                default:
                    return StatusCode((int)response.StatusCode, response);
            }
        }
    }
}
