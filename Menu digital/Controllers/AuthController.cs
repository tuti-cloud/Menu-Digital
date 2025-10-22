using Menu_Digital.Entities;
using Menu_Digital.Models.DTOs.Requests;
using Menu_Digital.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Menu_Digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public class AuthenticationController : ControllerBase
        {
            private readonly IConfiguration _config;
            private readonly IRestaurantService _restaurantService;

            public AuthenticationController(IConfiguration config, IRestaurantService restaurantService)
            {
                _config = config;
                _restaurantService = restaurantService;
            }

            [HttpPost]
            public IActionResult Authenticate([FromBody] CredentialRequestDto credentials)
            {
                Restaurant? restaurant = _restaurantService.Authenticate(credentials.Name, credentials.PasswordHash);

                if (restaurant is not null)
                {
                    var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

                    var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                    var claimsForToken = new List<Claim>
                    {
                        // new Claim("sub", user.Id.ToString()),
                        // new Claim("role", user.Role)
                    };

                    var jwtSecurityToken = new JwtSecurityToken(
                        _config["Authentication:Issuer"],
                        _config["Authentication:Audience"],
                        claimsForToken,
                        DateTime.UtcNow,
                        DateTime.UtcNow.AddHours(1),
                        signature
                    );

                    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    return Ok(tokenToReturn);
                }

                return Unauthorized();
            }
        }
    }
}