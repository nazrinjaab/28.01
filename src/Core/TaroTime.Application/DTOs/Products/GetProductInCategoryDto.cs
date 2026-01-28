namespace TaroTime.Application.DTOs
{
    public record GetProductInCategoryDto(
        long Id,
        string Name,
        decimal Price
        );
  
}
