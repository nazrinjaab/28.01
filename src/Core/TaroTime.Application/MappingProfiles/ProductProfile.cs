using AutoMapper;

using TaroTime.Application.DTOs;
using TaroTime.Application.DTOs.Products;
using TaroTime.Domain.Entities;

namespace TaroTime.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductInCategoryDto>();


            CreateMap<Product, GetProductItemDto>()
                .ForCtorParam(nameof(GetProductItemDto.CategoryName),
                opt => opt.MapFrom(p => p.Category.Name));


            CreateMap<Product, GetProductDto>().ForCtorParam(nameof(GetProductDto.CategoryDto),
                opt => opt.MapFrom(p => p.Category))


                .ForCtorParam(nameof(GetProductDto.TagDtos),
                opt => opt.MapFrom(p => p.ProductTags
                    .Select(pt => new GetTagInProductDto(pt.Tag.Id, pt.Tag.Name))
                    .ToList()));


            CreateMap<PostProductDto, Product>()
                .ForMember(
                p => p.ProductTags,
                opt => opt.MapFrom(pd => pd.TagIds
                                                .Select(ti => new ProductTag { TagId = ti }))
                );


            CreateMap<PutProductDto, Product>()
               .ForMember(
               p => p.ProductTags,
               opt => opt.MapFrom(pd => pd.TagIds
                                               .Select(ti => new ProductTag { TagId = ti }))
               );

        }
    }
}
