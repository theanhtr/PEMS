using Microsoft.AspNetCore.Mvc;
using PEMS.Application;
using PEMS.Domain;

namespace PEMS.Controllers
{
    /// <summary>
    /// Controller của phòng ban
    /// </summary>
    /// CreatedBy: TTANH (12/07/2024)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLoginParams)
        {
            var result = await _authService.Login(userLoginParams.Username, userLoginParams.Password);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegisterParams)
        {
            var result = await _authService.Register(userRegisterParams.Username, userRegisterParams.Password, userRegisterParams.Fullname);

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
