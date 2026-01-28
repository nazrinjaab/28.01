

namespace TaroTime.Application.DTOs
{
    public record GetCategoryDto(
        long Id,
        string Name,
        IEnumerable<GetProductInCategoryDto> ProductDtos
        );
   
}
