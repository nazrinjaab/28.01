using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities.common;
using TaroTime.Domain.Enums;

namespace TaroTime.Domain.Entities
{
    public class PalmReading:BaseAccountableEntity
    {
        public string UserId { get; set; }         
        public string Question { get; set; }        
        public string? Answer { get; set; }        
        public string? PalmReaderId { get; set; }   
        public PalmStatus Status { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string HandImagePath { get; set; }
        public string Result { get; set; }

    }
}
