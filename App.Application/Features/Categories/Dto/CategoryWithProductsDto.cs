using App.Application.Features.Products;


namespace App.Application.Features.Categories.Dto
{
    public record  CategoryWithProductsDto(int Id,string Name,List<ProductDto> Products)
    {
    }
}
