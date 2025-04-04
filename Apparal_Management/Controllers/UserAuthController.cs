//using Apparal_Management.Models.Dto;

//using Apparel_Management.Services.IServices;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace Apparel_Management.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserAuthController : ControllerBase
//    {
//        private readonly IUserAuthService _authService;

//        public UserAuthController(IUserAuthService authService)
//        {
//            _authService = authService;
//        }

//        [HttpPost("Register")]
//        public async Task<IActionResult> Register([FromBody] RegisterDto registerModel)
//        {
//            var result = await _authService.RegisterUser(registerModel);
//            if (result != null) return BadRequest(result);

//            return Ok("User registered successfully");
//        }

//        [HttpPost("Login")]
//        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
//        {
//            var token = await _authService.LoginUser(loginModel);
//            if (token == null) return Unauthorized("Invalid credentials");

//            return Ok(new { success = true, token });
//        }

//        [HttpPost("Logout")]
//        public async Task<IActionResult> Logout()
//        {
//            await _authService.LogoutUser();
//            return Ok("Logged out successfully");
//        }
//    }
//}






using Apparal_Management.Models.Dto;
using Apparel_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

//namespace Apparel_Management.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserAuthController : ControllerBase
//    {
//        private readonly IUserAuthService _authService;

//        public UserAuthController(IUserAuthService authService)
//        {
//            _authService = authService;
//        }

//        [HttpPost("Register")]
//        [AllowAnonymous]  // Allows registration without authentication
//        public async Task<IActionResult> Register([FromBody] RegisterDto registerModel)
//        {
//            var result = await _authService.RegisterUser(registerModel);
//            if (result != null) return BadRequest(result);

//            return Ok("User registered successfully");
//        }

//        [HttpPost("Login")]
//        [AllowAnonymous]  // Allows login without authentication
//        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
//        {
//            var token = await _authService.LoginUser(loginModel);
//            if (token == null) return Unauthorized("Invalid credentials");

//            return Ok(new { success = true, token });
//        }

//        [HttpPost("Logout")]
//        [Authorize]  // Requires authentication
//        public async Task<IActionResult> Logout()
//        {
//            await _authService.LogoutUser();
//            return Ok("Logged out successfully");
//        }
//    }
//}



using Apparal_Management.Models.Dto;
using Apparel_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Apparel_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _authService;

        public UserAuthController(IUserAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerModel)
        {
            var result = await _authService.RegisterUser(registerModel);
            if (result != null)
                return BadRequest(new { success = false, message = result });

            return Ok(new { success = true, message = "User registered successfully." });
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            var token = await _authService.LoginUser(loginModel);
            if (token == null)
                return Unauthorized(new { success = false, message = "Invalid credentials." });

            return Ok(new { success = true, message = "Login successful.", token });
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutUser();
            return Ok(new { success = true, message = "Logged out successfully." });
        }
    }
}