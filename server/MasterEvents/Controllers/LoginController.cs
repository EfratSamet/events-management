using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.mail) || string.IsNullOrWhiteSpace(loginDto.pass))
            {
                return BadRequest("Mail and password are required.");
            }

            var user = _loginService.Authenticate(loginDto.mail, loginDto.pass);
            if (user != null)
            {
                var token = _loginService.GenerateToken(user);
                return Ok(token);
            }
            return BadRequest("User does not exist or invalid credentials");
        }

    }
}
