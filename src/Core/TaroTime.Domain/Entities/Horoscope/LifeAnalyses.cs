using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Domain.Entities.Horoscope
{
    public class LifeAnalyses
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string ExpertId { get; set; }
        public AppUser Expert { get; set; }
        public ZodiacSign Zodiac { get; set; }
        public string ZodiacId { get; set; }
        public string Description { get; set; }
        public HoroscopeEntity Entity { get; set; }
        public DateTime UserBirthDate { get; set; }
        public TimeSpan BirthTime { get; set; }
        public string RisingSign { get; set; }   //Yukselen burc
        public string LifeAnalysis { get; set; }
    }
}
