using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Dtos;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IOrganizerService _service;
        private readonly IConfiguration _config;

        public LoginService(IOrganizerService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        public string GenerateToken(OrganizerDto organizer)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, organizer.name),
                new Claim(ClaimTypes.NameIdentifier, organizer.id.ToString()),
                new Claim(ClaimTypes.Email, organizer.mail),
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public OrganizerDto Authenticate(string mail, string pass)
        {
            var user = _service.GetAll().FirstOrDefault(x => x.mail == mail && x.password == pass);
            return user;
        }
    }
}