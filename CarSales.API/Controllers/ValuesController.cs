using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetValues()
        {
            var values = new[] { "Value1", "Value2", "Value3" };
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetValue(int id)
        {
            if (id < 0 || id >= 3)
            {
                return NotFound();
            }
            return Ok($"Value{id + 1}");
        }
        [HttpGet("Getss")]
        public IActionResult GetValuess()
        {
            var values = new[] { "Value1", "Value2", "Value3" };
            return Ok(values);
        }
    }
}
