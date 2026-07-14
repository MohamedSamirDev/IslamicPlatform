using IslamicPlatform.Domain.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Domain.Entites.Azkar
{
    public class UserZikrProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ZikrId { get; set; }
        public int CurrentCount { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime LastResetDate { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; }
        public Zikr Zikr { get; set; }
    }
}
