using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities.common;
using TaroTime.Domain.Enums;

namespace TaroTime.Domain.Entities
{
    public class TarotReading:BaseAccountableEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string? TarotReaderId { get; set; }
        public AppUser? TarotReader { get; set; }
        
        public string Question { get; set; }
        public string? Answer { get; set; }

        public TarotStatus Status { get; set; }
        public DateTime AcceptedAt { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
