//using Apparal_Management.Data;
//using Apparal_Management.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Apparal_Management.Services
//{
//    public class UserAuthService : IUserAuthService
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly string _jwtKey;
//        private readonly string? _jwtIssuer;
//        private readonly string? _jwtAudience;
//        private readonly int _jwtExpiry;

//        public UserAuthService(UserManager<ApplicationUser> userManager,
//                               SignInManager<ApplicationUser> signInManager,
//                               IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _jwtKey = configuration["Jwt:Key"];
//            _jwtIssuer = configuration["Jwt:Issuer"];
//            _jwtAudience = configuration["Jwt:Audience"];
//            _jwtExpiry = int.Parse(configuration["Jwt:ExpiryMinutes"]);
//        }

//        public async Task<string?> RegisterUser(RegisterModel registerModel)
//        {
//            if (registerModel == null ||
//                string.IsNullOrEmpty(registerModel.Name) ||
//                string.IsNullOrEmpty(registerModel.Email) ||
//                string.IsNullOrEmpty(registerModel.Password))
//            {
//                return "Invalid registration details";
//            }

//            var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);
//            if (existingUser != null)
//            {
//                return "Email already exists";
//            }

//            var user = new ApplicationUser
//            {
//                UserName = registerModel.Email,
//                Email = registerModel.Email,
//                Name = registerModel.Name
//            };

//            var result = await _userManager.CreateAsync(user, registerModel.Password);
//            if (!result.Succeeded)
//            {
//                return "User registration failed";
//            }

//            return null; // No error, registration successful
//        }

//        public async Task<string?> LoginUser(LoginModel loginModel)
//        {
//            var user = await _userManager.FindByEmailAsync(loginModel.Email);
//            if (user == null)
//            {
//                return "Invalid username or password";
//            }

//            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
//            if (!result.Succeeded)
//            {
//                return "Invalid username or password";
//            }

//            return GenerateJwtToken(user);
//        }

//        public async Task LogoutUser()
//        {
//            await _signInManager.SignOutAsync();
//        }

//        private string GenerateJwtToken(ApplicationUser user)
//        {
//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
//                new Claim(JwtRegisteredClaimNames.Email, user.Email),
//                new Claim("Name", user.Name),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(_jwtExpiry),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
