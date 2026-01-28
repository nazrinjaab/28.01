
using TaroTime.Application.DTOs;
using TaroTime.Application.DTOs.Products;

namespace TaroTime.Application.Interfaces.Services

{
    public interface IProductService
    {
        Task<IReadOnlyList<GetProductItemDto>> GetAllAsync(int page, int take);
        Task<GetProductDto> GetByIdAsync(long id);
        Task CreateProductAsync(PostProductDto postProductDto);
        Task UpdateProductAsync(long id, PutProductDto putProductDto);
        Task DeleteAsync(long id);
        Task SoftDeleteAsync(long id);
    }
}
