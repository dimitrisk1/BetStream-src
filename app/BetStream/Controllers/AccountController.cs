using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetStream.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminAccountInfo()
        {
            var accountInfo = new
            {
                Username = User.Identity?.Name ?? string.Empty,
                Email = string.Empty
            };

            return Ok(accountInfo);
        }
    }
}
