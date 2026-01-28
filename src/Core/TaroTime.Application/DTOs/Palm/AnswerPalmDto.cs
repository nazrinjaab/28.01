using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Palm
{
   public record AnswerPalmDto(
       [Required]
        long PalmId,
        [Required]
        string Answer
       );
    
}
