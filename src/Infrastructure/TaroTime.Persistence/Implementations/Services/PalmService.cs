using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Palm;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Persistence.Implementations.Services
{
    internal class PalmService : IPalmService
    {
        private readonly IPalmRepository _repository;
        private readonly IWebHostEnvironment _env;

        public PalmService(IPalmRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task CreateRequestAsync(string userId, CreatePalmRequestDto dto)
        {
            if (dto.HandImage == null || dto.HandImage.Length == 0)
                throw new Exception("Hand image is required");

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot"), "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(dto.HandImage.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.HandImage.CopyToAsync(stream);
            }

            var palm = new PalmReading
            {
                UserId = userId,
                Question = dto.Question,
                HandImagePath = "/uploads/" + fileName,
                Status = PalmStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _repository.Add(palm);
            await _repository.SaveChangesAsync();
        }

        public async Task AcceptAsync(long palmId, string readerId)
        {
            var palm = await _repository.GetByIdAsync(palmId)
                ?? throw new Exception("Palm reading not found");

            if (palm.Status != PalmStatus.Pending)
                throw new Exception("Already accepted or completed");

            palm.PalmReaderId = readerId;
            palm.Status = PalmStatus.Accepted;
            palm.AcceptedAt = DateTime.UtcNow;

            _repository.Update(palm);
            await _repository.SaveChangesAsync();
        }

        public async Task AnswerAsync(string readerId, AnswerPalmDto dto)
        {
            var palm = await _repository.GetByIdAsync(dto.PalmId)
                ?? throw new Exception("Palm reading not found");

            if (palm.PalmReaderId != readerId)
                throw new Exception("You are not assigned to this palm reading");

            palm.Answer = dto.Answer;
            palm.Status = PalmStatus.Completed;
            palm.CompletedAt = DateTime.UtcNow;

            _repository.Update(palm);
            await _repository.SaveChangesAsync();
        }
    }
}
