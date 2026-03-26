using AutoMapper;
using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Result<AuthResultDto>> RegisterAsync(RegisterDto dto)
        {
            var existingByPhone = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == dto.PhoneNumber);
            if (existingByPhone != null)
            {
                return Result<AuthResultDto>.Failure("Phone number already in use");
            }
            var user = _mapper.Map<ApplicationUser>(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<AuthResultDto>.Failure(errors);
            }
            await _userManager.AddToRoleAsync(user, "Customer");
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var result2= new AuthResultDto
            {
                IsSuccess = true,
                UserId = user.Id,
                Phone = user.PhoneNumber,
                FullName = user.FullName,
                Role = role

            };
            return Result<AuthResultDto>.Success(result2);
        }
        public async Task<Result<AuthResultDto?>> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.Users
          .FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);

            if (user == null)
            { 
                return Result<AuthResultDto?>.Failure("Invalid phone number or password");
            }

            var ok = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!ok)
            {
                return Result<AuthResultDto?>.Failure("Invalid phone number or password");
            }
            //Generate Access Token
            var token = await _tokenService.GenerateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var result2= new AuthResultDto
            {
                Token = token,
                //RefreshToken=refreshToken,
                UserId = user.Id,
                Role = role,
                IsSuccess = true,
                Phone = user.PhoneNumber,
                FullName = user.FullName
            };
            return Result<AuthResultDto?>.Success(result2);
        }
      
    }
}
