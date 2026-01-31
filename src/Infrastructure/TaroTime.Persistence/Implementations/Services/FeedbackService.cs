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
using TaroTime.Persistence.Contexts;

namespace TaroTime.Persistence.Implementations.Services
{
    internal class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;
        private readonly AppDbContext _context;

        public FeedbackService(IFeedbackRepository repository,
            AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task SubmitAsync(FeedbackDto dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Message))
                throw new Exception("Mesaj boş ola bilməz.");

            var userExists = await _context.Users
        .AnyAsync(u => u.UserName == dto.UserName && u.Email == dto.Email);
            if (!userExists)
            {
                throw new Exception("username or email not found");
            }
            

            var feedback = new Feedback
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Type = dto.Type,   
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow
            };

            _repository.Add(feedback);
            await _repository.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<FeedbackDto>> GetAllAsync()
        {
            var feedbacks = await _repository
                .GetAll(
                    sort: f => f.CreatedAt,
                    includes: null
                )
                .ToListAsync();

           
            var result = feedbacks.Select(f => new FeedbackDto(

                f.UserName,
                f.Email,
                f.Type, 
                f.Message,
                f.CreatedAt
            )).ToList();

            return result;
        }


        public async Task<Feedback?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
