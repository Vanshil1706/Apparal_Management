using Apparal_Management.Models.Dto;

namespace Apparal_Management.Services.IServices
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> CreateUserAsync(CreateUserDto userDto);
        Task<UserDto> UpdateUserAsync(string id, UpdateUserDto userDto);
        Task<bool> DeleteUserAsync(string id);
    }
}
