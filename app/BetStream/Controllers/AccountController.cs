using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetStream.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminAccountInfo()
        {

            var accountInfo = new
            {
                Username = User.Identity.Name,
                Email = ""
            };
            return Ok(accountInfo);
        }
    }
}
