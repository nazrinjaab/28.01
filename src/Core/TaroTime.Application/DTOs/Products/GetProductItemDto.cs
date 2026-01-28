namespace TaroTime.Application.DTOs
{
    public record GetProductItemDto(
        long Id,
        string Name,
        decimal Price,
        string CategoryName
        );
   
}
