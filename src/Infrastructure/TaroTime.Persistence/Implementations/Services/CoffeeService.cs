using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Coffees;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Persistence.Implementations.Services
{
    internal class CoffeeService : ICoffeeService
    {
        private readonly ICoffeeRepository _repository;
        private readonly IWebHostEnvironment _env;

        public CoffeeService(ICoffeeRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task CreateRequestAsync(string userId, CreateCoffeeRequestDto dto)
        {
            if (dto.CupImage == null || dto.CupImage.Length == 0)
                throw new Exception("Cup image is required");

            
            var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot"), "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(dto.CupImage.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.CupImage.CopyToAsync(stream);
            }

            var coffee = new CoffeeReading
            {
                UserId = userId,
                Question = dto.Question,
                CupImagePath = "/uploads/" + fileName,
                Status = CoffeeStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

             _repository.Add(coffee);
            await _repository.SaveChangesAsync();
        }

        public async Task AcceptAsync(long coffeeId, string readerId)
        {
            var coffee = await _repository.GetByIdAsync(coffeeId)
                ?? throw new Exception("Coffee reading not found");

            if (coffee.Status != CoffeeStatus.Pending)
                throw new Exception("Already accepted or completed");

            coffee.CoffeeReaderId = readerId;
            coffee.Status = CoffeeStatus.Accepted;
            coffee.AcceptedAt = DateTime.UtcNow;

            _repository.Update(coffee);
            await _repository.SaveChangesAsync();
        }

        public async Task AnswerAsync(string readerId, AnswerCoffeeDto dto)
        {
            var coffee = await _repository.GetByIdAsync(dto.CoffeeId)
                ?? throw new Exception("Coffee reading not found");

            if (coffee.CoffeeReaderId != readerId)
                throw new Exception("You are not assigned to this coffee reading");

            coffee.Answer = dto.Answer;
            coffee.Status = CoffeeStatus.Completed;
            coffee.CompletedAt = DateTime.UtcNow;

            _repository.Update(coffee);
            await _repository.SaveChangesAsync();
        }
    }
}
