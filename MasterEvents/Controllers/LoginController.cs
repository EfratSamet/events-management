using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Entity;
using Service.Dtos;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IService<OrganizerDto> service;
        private readonly IConfiguration config;
        public LoginController(IService<OrganizerDto> service, IConfiguration config)
        {
            this.service = service;
            this.config = config;
        }
        // GET: api/<LoginController>
        [HttpGet]
        public List<OrganizerDto> Get()
        {
            return service.GetAll();
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public OrganizerDto Get(string id)
        {
            return service.Get(id);
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post([FromBody] string mail, [FromQuery] string pass)
        {
            var user = Authenticate(mail, pass);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return BadRequest("user does not exist");
        }
        private string Generate(OrganizerDto organizer)
        {
            //הקוד להצפנה במערך של ביטים 
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            //אלגוריתם להצפנה
            var carditional = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,organizer.name),
                new Claim(ClaimTypes.NameIdentifier,organizer.id.ToString()),
                new Claim(ClaimTypes.Email,organizer.mail),
            };

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"], config["Jwt:Audience"]
                , claims,
          expires: DateTime.Now.AddMinutes(45),
              signingCredentials: carditional);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private OrganizerDto Authenticate(string mail, string pass)
        {
            var user = service.GetAll().FirstOrDefault(x => x.mail == mail && x.password == pass);
            return user;
        }
        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] OrganizerDto value)
        {
            service.Update(id,value);
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            service.Delete(id);
        }
    }
}
