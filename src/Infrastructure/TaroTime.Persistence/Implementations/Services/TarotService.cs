using TaroTime.Application.DTOs.Tarots;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Persistence.Implementations.Services
{
    public class TarotService : ITarotService
    {
        private readonly ITarotRepository _repository;

        public TarotService(ITarotRepository repository)
        {
            _repository = repository;
            
        }

        public async Task CreateRequestAsync(string userId, CreateTarotRequestDto dto)
        {
            var tarot = new TarotReading
            {
                UserId = userId,
                Question = dto.Question,
                Status = TarotStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _repository.Add(tarot);
            await _repository.SaveChangesAsync();
        }

        public async Task AcceptAsync(long tarotId, string tarotReaderId)
        {

            var tarot = await _repository.GetByIdAsync(tarotId)
                ?? throw new Exception("Tarot not found");

            if (tarot.Status != TarotStatus.Pending)
                throw new Exception("Tarot already accepted or completed");

            tarot.TarotReaderId = tarotReaderId;
            tarot.Status = TarotStatus.Accepted;
            tarot.AcceptedAt = DateTime.UtcNow;

            _repository.Update(tarot);
            await _repository.SaveChangesAsync();
        }

        public async Task AnswerAsync(string tarotReaderId, AnswerTarotDto dto)
        {
            var tarot = await _repository.GetByIdAsync(dto.TarotId)
                ?? throw new Exception("Tarot not found");

            if (tarot.TarotReaderId != tarotReaderId)
                throw new Exception("You are not assigned to this tarot");

            tarot.Answer = dto.Answer;
            tarot.Status = TarotStatus.Completed;
            tarot.CompletedAt = DateTime.UtcNow;

            _repository.Update(tarot);
            await _repository.SaveChangesAsync();
        }
    }
}

