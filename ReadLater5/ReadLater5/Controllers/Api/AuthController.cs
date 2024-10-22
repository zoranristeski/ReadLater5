using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReadLater5.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReadLater5.Controllers.Api
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationApiModel authenticationApiModel)
        {
            var apiKey = _configuration["ApiKey"];

            if(string.IsNullOrEmpty(authenticationApiModel.ApiKey) || apiKey != authenticationApiModel.ApiKey) 
            {
                return NotFound();
            }
       
            var tokenResponse = new TokenResponseApiModel
            {
                AccessToken = GenerateJwtToken()
            };

            return Ok(tokenResponse);
        }

        private string GenerateJwtToken()
        {
            var apiKey = _configuration.GetValue<string>("ApiKey");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(apiKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Webpage, "https://ReadLaterTest.com/"),
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = "Read Later 5",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
