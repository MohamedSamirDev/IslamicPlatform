using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Identity
{
   
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRevoked { get; set; } = false;
        public bool IsUsed { get; set; } = false;
        public string? DeviceName { get; set; }
        public string? ReplacedByToken { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
