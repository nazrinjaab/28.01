using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Horoscope;
using TaroTime.Application.Interfaces.Repositories.Horoscope;
using TaroTime.Application.Interfaces.Services.Horoscope;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Persistence.Implementations.Services.Horoscope
{
    internal class CompatibilityZodiacService:ICompatibilityZodiacService
    {
        private readonly ICompatibilityZodiacRepository _repository;

        public CompatibilityZodiacService(ICompatibilityZodiacRepository compatibilityRepository)
        {
            _repository = compatibilityRepository;
        }

        public async Task CreateCompatibilityAsync(string userId, CreateCompatibilityDto dto)
        {
            var compatibility = new CompatibilityZodiac
            {
                UserId = userId,
                UserZodiacId = dto.UserZodiac.ToString(),
                PartnerZodiacId = dto.PartnerZodiac.ToString(),
                UserBirthDate = dto.UserBirthDate,
                PartnerBirthDate = dto.PartnerBirthDate,
                CompatibilityPercent = 0,
                Description = string.Empty
            };

            _repository.Add(compatibility);
            await _repository.SaveChangesAsync();
        }
    }
}
