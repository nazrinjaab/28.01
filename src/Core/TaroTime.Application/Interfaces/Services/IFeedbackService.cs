using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.FeedBack;
using TaroTime.Domain.Entities;

namespace TaroTime.Application.Interfaces.Services
{
    public interface IFeedbackService
    {
        Task SubmitAsync(FeedbackDto dto);
        Task<IReadOnlyList<FeedbackDto>> GetAllAsync();
        Task<Feedback?> GetByIdAsync(int id);
    }
}
