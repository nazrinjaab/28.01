using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Palm;

namespace TaroTime.Application.Interfaces.Services
{
    public interface IPalmService
    {
        Task CreateRequestAsync(string userId, CreatePalmRequestDto dto);
        Task AcceptAsync(long palmId, string readerId);
        Task AnswerAsync(string readerId, AnswerPalmDto dto);
        Task<IReadOnlyList<PalmReadingDto>> GetAllAsync();
        Task<IEnumerable<PalmReadingDto>> GetByUserIdAsync(string userId);

    }
}
