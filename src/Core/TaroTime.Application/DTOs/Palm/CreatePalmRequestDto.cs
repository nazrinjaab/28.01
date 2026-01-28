using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Palm
{
    public record CreatePalmRequestDto(
        [Required]
        string Question,
        [Required]
        IFormFile HandImage
        );
    
}
