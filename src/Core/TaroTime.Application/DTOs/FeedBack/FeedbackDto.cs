using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.FeedBack
{
    public record FeedbackDto(
        long Id,
        string? UserName,
        string? Email,
        FeedbackType Type,
        string? Message,
        DateTime CreatedAt
       );
 }
