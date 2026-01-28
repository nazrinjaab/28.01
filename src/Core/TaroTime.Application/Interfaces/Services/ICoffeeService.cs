using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Coffees;

namespace TaroTime.Application.Interfaces.Services
{
    public interface ICoffeeService
    {
        Task CreateRequestAsync(string userId, CreateCoffeeRequestDto dto);
        Task AcceptAsync(long coffeeId, string readerId);
        Task AnswerAsync(string readerId, AnswerCoffeeDto dto);
    }
}
