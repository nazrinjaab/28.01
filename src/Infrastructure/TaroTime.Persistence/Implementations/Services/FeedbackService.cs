using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Appointments;
using TaroTime.Application.DTOs.FeedBack;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;

namespace TaroTime.Persistence.Implementations.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;

        public FeedbackService(IFeedbackRepository repository)
        {
            _repository = repository;
        }

        public async Task SubmitAsync(FeedbackDto dto)
        {
            var feedback = new Feedback
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Type = dto.Type,   // Enum: Təklif və ya Şikayət
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow
            };

             _repository.Add(feedback);
        }

        public async Task<IReadOnlyList<FeedbackDto>> GetAllAsync()
        {
            var feedbacks = await _repository
                .GetAll(
                    sort: f => f.CreatedAt,
                    includes: null
                )
                .ToListAsync();

            // Manual map → entity → DTO
            var result = feedbacks.Select(f => new FeedbackDto(

                f.UserName,
                f.Email,
                f.Type.ToString(), // Enum → string
                f.Message,
                f.CreatedAt
            )).ToList();

            return result;
        }
       

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
