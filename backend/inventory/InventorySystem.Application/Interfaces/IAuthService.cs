using InventorySystem.Application.Common;
using InventorySystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResultDto>> RegisterAsync(RegisterDto dto);
        Task<Result<AuthResultDto?>> LoginAsync(LoginDto dto);
    }
}
