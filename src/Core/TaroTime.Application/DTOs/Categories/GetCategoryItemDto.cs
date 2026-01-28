namespace TaroTime.Application.DTOs
{
    public record GetCategoryItemDto(
        long Id,
        string Name,
        int ProductCount
        );
    
}
