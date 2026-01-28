using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Tarots;

namespace TaroTime.Application.Interfaces.Services
{
    public interface ITarotService
    {
        Task CreateRequestAsync(string userId, CreateTarotRequestDto dto);
        Task AcceptAsync(long tarotId, string tarotReaderId);
        Task AnswerAsync(string tarotReaderId, AnswerTarotDto dto);
    }
}
