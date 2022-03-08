using ExcerciseApp.API.Models;
using ExcerciseApp.API.Models.Requests.Account;
using ExcerciseApp.API.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExcerciseApp.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<APIResponse<string>> GenerateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new APIResponse<string>
            {
                Result = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            };
        }

        public async Task<APIResponse<bool>> ValidateUserAsync(LoginRequest request)
        {
            _user = await _userManager.FindByNameAsync(request.Email);
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(_user, request.Password);

            return new APIResponse<bool>
            {
                Result = _user != null && isPasswordCorrect,
            };
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenExpiry = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:lifetime"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: tokenExpiry,
                signingCredentials: signingCredentials);

            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName),
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("KEY", EnvironmentVariableTarget.User);
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
    }
}
