using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetStream.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok("BetStream is up.");
        }

        [HttpGet("error")]
        public IActionResult GetError()
        {
           return StatusCode(500, "Simulated error for testing.");
        }
    }
}
