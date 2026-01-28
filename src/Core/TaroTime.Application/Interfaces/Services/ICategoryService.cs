using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs;

namespace TaroTime.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<GetCategoryItemDto>> GetAllAsync(int page, int take);
        Task<GetCategoryDto> GetByIdAsync(long id);
        Task CreateAsync(PostCategoryDto categoryDto);
        Task UpdateAsync(long id, PutCategoryDto categoryDto);
        Task DeleteAsync(long id);
        Task SoftDeleteAsync(long id);
    }
}
