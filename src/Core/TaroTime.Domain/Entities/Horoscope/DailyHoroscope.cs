using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Domain.Entities
{
    public class DailyHoroscope
    {
        public string ExpertId { get; set; }
        public AppUser Expert { get; set; }
        public ZodiacSign Zodiac { get; set; }
        public string ZodiacId { get; set; }
        public ZodiacElement Element { get; set; }
        public string Description { get; set; }
        public DateTime Today { get; set; }
    }
}
