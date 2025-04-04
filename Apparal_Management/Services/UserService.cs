//using Apparal_Management.Models.Dto;
//using Apparal_Management.Models;
//using Apparal_Management.Services;
//using Apparel_Management.Services.IServices;
//using Microsoft.AspNetCore.Identity;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Apparel_Management.Services
//{

//    public class UserService : IUserService
//    {
//        private readonly UserManager<ApplicationUser> _userManager;

//        public UserService(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        public async Task<IEnumerable<UserDto>> GetAllUsers()
//        {
//            var users = _userManager.Users.ToList();
//            return users.Select(u => new UserDto { Id = u.Id, Email = u.Email, Name = u.Name });
//        }

//        public async Task<UserDto?> GetUserById(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            return user == null ? null : new UserDto { Id = user.Id, Email = user.Email, Name = user.Name };
//        }

//        public async Task<CreateUserDto?> CreateUser(CreateUserDto createUserDto)
//        {
//            var user = new ApplicationUser { UserName = createUserDto.Email, Email = createUserDto.Email, Name = createUserDto.Name };
//            var result = await _userManager.CreateAsync(user, createUserDto.Password);
//            return result.Succeeded ? new CreateUserDto {Email = user.Email, Name = user.Name } : null;
//        }

//        public async Task<UpdateUserDto?> UpdateUser(string id, UpdateUserDto updateUserDto)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user == null) return null;

//            user.Name = updateUserDto.Name;
//            await _userManager.UpdateAsync(user);
//            return new UpdateUserDto { Email = user.Email, Name = user.Name };
//        }

//        public async Task<bool> DeleteUser(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            return user != null && (await _userManager.DeleteAsync(user)).Succeeded;
//        }
//    }
//}


using Microsoft.AspNetCore.Identity;
using Apparal_Management.Models;
using Apparal_Management.Models.Dto;
using Apparal_Management.Services.IServices;

namespace Apparal_Management.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = _userManager.Users
                .Select(u => new UserDto { Id = u.Id, Name = u.Name, Email = u.Email })
                .ToList();
            return users;
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
        {
            var user = new ApplicationUser
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                Name = userDto.Name
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded) return null;

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.UserName = userDto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return null;

            return new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}

