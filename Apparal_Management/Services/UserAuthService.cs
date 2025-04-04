using Apparal_Management.Models.Dto;
using Apparal_Management.Models;
using Apparel_Management.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Apparel_Management.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _jwtKey;
        private readonly int _jwtExpiry;
        private string _jwtAudience;
        private string _jwtIssuer;

        public UserAuthService(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager,
                               IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtKey = configuration["Jwt:Key"];
            _jwtExpiry = int.TryParse(configuration["Jwt:ExpiryMinutes"], out var expiry) ? expiry : 60;
        }

        public async Task<string?> RegisterUser(RegisterDto registerModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);
            if (existingUser != null) return "Email already exists";

            var user = new ApplicationUser { 
                UserName = registerModel.Email, 
                Email = registerModel.Email, 
                Name = registerModel.Name 
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            return result.Succeeded ? null : result.ToString();//"User registration failed";
        }

        public async Task<string?> LoginUser(LoginDto loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) return "Invalid credentials";

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
            return result.Succeeded ? GenerateJwtToken(user) : "Invalid credentials";
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        //private string GenerateJwtToken(ApplicationUser user)
        //{
        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //        new Claim("Name", user.Name),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };


        //    //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(_jwtExpiry));
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
        new Claim("Name", user.Name ?? ""),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtExpiry),
                signingCredentials: null // 🔹 No signing credentials (NO SIGNATURE)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}