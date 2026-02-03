using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Horoscope;

namespace TaroTime.Application.Interfaces.Services.Horoscope
{
    public interface ICompatibilityZodiacService
    {
        Task CreateCompatibilityAsync(string userId, CreateCompatibilityDto dto);
        //Task<IEnumerable<ShowToExpertDto>> GetByExpertIdAsync(string expertId);
        Task AcceptAsync(long compatibilityId, string expertId);
        Task AnswerAsync(string expertId, AnswerCompatibilityDto dto);
        Task<IReadOnlyList<HoroscopeReadingDto>> GetAllAsync();
        Task<IEnumerable<HoroscopeReadingDto>> GetByUserIdAsync(string userId);
        Task<IEnumerable<HoroscoperDto>> GetByReaderIdAsync(string readerId);

    }
}
