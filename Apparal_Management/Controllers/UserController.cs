using Microsoft.AspNetCore.Mvc;
using Apparal_Management.Models.Dto;
using Apparal_Management.Services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace Apparal_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.CreateUserAsync(userDto);
            if (user == null) return BadRequest("User creation failed.");
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedUser = await _userService.UpdateUserAsync(id, userDto);
            if (updatedUser == null) return NotFound();
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}




//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Apparal_Management.Models.Dto;
//using Apparal_Management.Services.IServices;
//using System.Threading.Tasks;

//namespace Apparal_Management.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]  // Protect all routes in this controller
//    public class UserController : ControllerBase
//    {
//        private readonly IUserService _userService;

//        public UserController(IUserService userService)
//        {
//            _userService = userService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetUsers()
//        {
//            var users = await _userService.GetUsersAsync();
//            return Ok(new { success = true, message = "Users retrieved successfully.", data = users });
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetUser(string id)
//        {
//            var user = await _userService.GetUserByIdAsync(id);
//            if (user == null) return NotFound(new { success = false, message = "User not found." });

//            return Ok(new { success = true, message = "User retrieved successfully.", data = user });
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);

//            var user = await _userService.CreateUserAsync(userDto);
//            if (user == null) return BadRequest(new { success = false, message = "User creation failed." });

//            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new { success = true, message = "User created successfully.", data = user });
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto userDto)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);

//            var updatedUser = await _userService.UpdateUserAsync(id, userDto);
//            if (updatedUser == null) return NotFound(new { success = false, message = "User update failed. User not found." });

//            return Ok(new { success = true, message = "User updated successfully.", data = updatedUser });
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUser(string id)
//        {
//            var result = await _userService.DeleteUserAsync(id);
//            if (!result) return NotFound(new { success = false, message = "User deletion failed. User not found." });

//            return Ok(new { success = true, message = "User deleted successfully." });
//        }
//    }
//}




//using Microsoft.AspNetCore.Mvc;
//using Apparal_Management.Models.Dto;
//using Apparal_Management.Services.IServices;
//using Microsoft.AspNetCore.Authorization;
//using System.Threading.Tasks;

//namespace Apparal_Management.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserService _userService;

//        public UserController(IUserService userService)
//        {
//            _userService = userService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetUsers()
//        {
//            var users = await _userService.GetUsersAsync();
//            return Ok(new { success = true, message = "Users retrieved successfully.", data = users });
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetUser(string id)
//        {
//            var user = await _userService.GetUserByIdAsync(id);
//            if (user == null)
//                return NotFound(new { success = false, message = "User not found." });

//            return Ok(new { success = true, message = "User retrieved successfully.", data = user });
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(new { success = false, message = "Invalid input data.", errors = ModelState });

//            var user = await _userService.CreateUserAsync(userDto);
//            if (user == null)
//                return BadRequest(new { success = false, message = "User creation failed." });

//            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new { success = true, message = "User created successfully.", data = user });
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto userDto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(new { success = false, message = "Invalid input data.", errors = ModelState });

//            var updatedUser = await _userService.UpdateUserAsync(id, userDto);
//            if (updatedUser == null)
//                return NotFound(new { success = false, message = "User not found or update failed." });

//            return Ok(new { success = true, message = "User updated successfully.", data = updatedUser });
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUser(string id)
//        {
//            var result = await _userService.DeleteUserAsync(id);
//            if (!result)
//                return NotFound(new { success = false, message = "User not found." });

//            return Ok(new { success = true, message = "User deleted successfully." });
//        }
//    }
//}

