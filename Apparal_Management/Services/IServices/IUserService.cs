using Apparal_Management.DTOs;

namespace Apparal_Management.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto?> GetUserById(string id);
        Task<UserDto?> CreateUser(CreateUserDto createUserDto);
        Task<UserDto?> UpdateUser(string id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUser(string id);
    }
}