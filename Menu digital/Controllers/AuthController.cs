using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Menu_Digital.Services.Interfaces;

namespace Menu_Digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService; //no si la inyección correcta es user? supongo que si :)
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

    }
}
