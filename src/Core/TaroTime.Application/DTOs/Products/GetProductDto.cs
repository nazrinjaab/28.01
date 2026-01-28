namespace TaroTime.Application.DTOs
{
    public record GetProductDto(
        long Id,
        string Name,
        decimal Price,
        string SKU,
        string Description,
        GetCategoryInProductDto CategoryDto,
        ICollection<GetTagInProductDto> TagDtos
        );
    
}
