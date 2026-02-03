using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Horoscope;
using TaroTime.Application.DTOs.Palm;
using TaroTime.Application.DTOs.Tarots;
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
                Status = CompatibilityStatus.Pending,
                CompatibilityPercent = 0,
                Description = string.Empty
            };

            _repository.Add(compatibility);
            await _repository.SaveChangesAsync();
        }

       
        public async Task AcceptAsync(long compatibilityId, string expertId)
        {
            var compatibility = await _repository.GetByIdAsync(compatibilityId)
                ?? throw new Exception("Compatibility request not found");

            if (compatibility.Status != CompatibilityStatus.Pending)
                throw new Exception("Compatibility already accepted or completed");

            compatibility.ExpertId = expertId;
            compatibility.Status = CompatibilityStatus.Accepted;
            compatibility.AcceptedAt = DateTime.UtcNow;

            _repository.Update(compatibility);
            await _repository.SaveChangesAsync();
        }


        public async Task AnswerAsync(string expertId, AnswerCompatibilityDto dto)
        {
            var compatibility = await _repository.GetByIdAsync(dto.CompatibilityId)
                ?? throw new Exception("Compatibility request not found");

            if (compatibility.ExpertId != expertId)
                throw new Exception("You are not assigned to this compatibility request");

            compatibility.CompatibilityPercent = dto.CompatibilityPercent;
            compatibility.Description = dto.Description;
            compatibility.Status = CompatibilityStatus.Completed;
            compatibility.CompletedAt = DateTime.UtcNow;

            _repository.Update(compatibility);
            await _repository.SaveChangesAsync();
        }



        //public async Task<IEnumerable<HoroscopeReadingDto>> GetByUserIdAsync(string userId)
        //{
        //    var compatibilities = await _repository
        //        .GetAll(sort: c => c.CreatedAt, includes: null)
        //        .Where(x => x.UserId == userId)
        //        .ToListAsync();

        //    return compatibilities.Select(x => new HoroscopeReadingDto(
        //        x.Id,
        //        x.UserId,
        //        x.ExpertId,
        //        x.Description
        //    ));
        //}
        

        
        public async Task<IReadOnlyList<HoroscopeReadingDto>> GetAllAsync()
        {
            var compatibilities = await _repository
                .GetAll(sort: c => c.CreatedAt, includes: null)
                .ToListAsync();

            var result = compatibilities.Select(x => new HoroscopeReadingDto(
               x.Id,
               x.UserId,
               x.ExpertId,
               x.Description
            )).ToList();

            return result;
        }


        public async Task<IEnumerable<HoroscopeReadingDto>> GetByUserIdAsync(string userId)
        {
            var palms = await _repository
        .GetAll(
            sort: p => p.CreatedAt,
            includes: null
        )
        .Where(x => x.UserId == userId)
        .ToListAsync();

            var result = palms.Select(x => new HoroscopeReadingDto(
                  x.Id,
               x.UserId,
               x.ExpertId,
               x.Description
            ));

            return result;
        }

        public async Task<IEnumerable<HoroscoperDto>> GetByReaderIdAsync(string readerId)
        {
            var palms = await _repository
                .GetAll(sort: p => p.CreatedAt, includes: null)
                .Where(x => x.ExpertId == readerId)
                .ToListAsync();

            return palms.Select(x => new HoroscoperDto(
                x.Id,
                 x.UserId,
         x.Description,
         x.UserZodiacId,
         x.PartnerZodiacId,
         x.Status,
         x.CreatedAt
            ));
        }
       
    }
   


    }

