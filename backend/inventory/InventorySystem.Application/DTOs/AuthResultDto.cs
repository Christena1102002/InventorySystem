using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Application.DTOs
{
        public class AuthResultDto
        {
            public bool IsSuccess { get; set; }
            public string Token { get; set; }

            public string Role { get; set; }
            public string UserId { get; set; }
            public string Phone { get; set; }
            public string FullName { get; set; }
        }
  }
