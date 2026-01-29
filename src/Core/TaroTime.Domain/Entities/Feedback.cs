using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities.common;
using TaroTime.Domain.Enums;

namespace TaroTime.Domain.Entities
{
    public class Feedback:BaseAccountableEntity
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public FeedbackType Type { get; set; }   
        public string? Message { get; set; }
       
    }
}
