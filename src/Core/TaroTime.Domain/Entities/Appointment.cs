using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities.common;
using TaroTime.Domain.Enums;

namespace TaroTime.Domain.Entities
{
    public class Appointment:BaseAccountableEntity
    {
        public string UserId { get; set; }          
        public AppUser User { get; set; }

        public string ExpertId { get; set; }        //psixoloq ve ya falçı
        public AppUser Expert { get; set; }

        public DateTime StartTime { get; set; }    
        public DateTime EndTime { get; set; }       

        public AppointmentStatus Status { get; set; }
        public MeetingType MeetingType { get; set; }
        public string? MeetingLink { get; set; }

    }
}
