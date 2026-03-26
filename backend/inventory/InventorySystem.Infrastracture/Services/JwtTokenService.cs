using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastracture.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSettings _settings;
        private readonly UserManager<ApplicationUser> _userManager;
        public JwtTokenService(IOptions<JwtSettings> options, UserManager<ApplicationUser> userManager)
        {
            _settings = options.Value;
            _userManager = userManager;
        }
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.FullName ?? "")
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes);


            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
