using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities.common;
using TaroTime.Domain.Enums;

namespace TaroTime.Domain.Entities
{
    public class CompatibilityZodiac:BaseAccountableEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string? ExpertId { get; set; }
        public AppUser? Expert { get; set; }
        public ZodiacSign UserZodiac { get; set; }
        public ZodiacSign PartnerZodiac { get; set; }
        public string UserZodiacId { get; set; }
        public string PartnerZodiacId { get; set; }
        public DateTime UserBirthDate { get; set; }
        public DateTime PartnerBirthDate { get; set; }
        public int CompatibilityPercent { get; set; }
        public string Description { get; set; }
        public CompatibilityStatus Status { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

    }
}
