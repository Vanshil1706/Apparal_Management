using Apparal_Management.Models.Dto;
//using Apparal_Management.Models.LoginDto;
//using Apparel_Management.DTOs;
using System.Threading.Tasks;

namespace Apparel_Management.Services.IServices
{
    public interface IUserAuthService
    {
        Task<string?> RegisterUser(RegisterDto registerModel);
        Task<string?> LoginUser(LoginDto loginModel);
        Task LogoutUser();
    }
}
