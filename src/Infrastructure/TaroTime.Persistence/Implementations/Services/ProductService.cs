using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaroTime.Application.DTOs;
using TaroTime.Application.DTOs.Products;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;

namespace TaroTime.Persistence.Implementations.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IMapper mapper
            )
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GetProductItemDto>> GetAllAsync(int page, int take)
        {
            IReadOnlyList<Product> products = await _repository.GetAll(
                  page: page,
                  take: take,
                  includes: nameof(Product.Category)
                  ).ToListAsync();

            return _mapper.Map<IReadOnlyList<GetProductItemDto>>(products);
        }

        public async Task<GetProductDto> GetByIdAsync(long id)
        {
            Product product = await _repository.GetByIdAsync(id, "ProductTags.Tag", nameof(Product.Category));
            if (product != null)
            {
                throw new Exception("entity not found");
            }

            return _mapper.Map<GetProductDto>(product);
        }

        public async Task CreateProductAsync (PostProductDto postProductDto)
        {
            bool result = await _repository.AnyAsync(p => p.Name == postProductDto.Name);
            if (result)
            {
                throw new Exception("Product already exists");
            }

            bool categoryResult = await _categoryRepository.AnyAsync(c => c.Id == postProductDto.CategoryId);
            if (!categoryResult)
            {
                throw new Exception("category doesnt exist");
            }

           var tags = await _tagRepository.GetAll(t=>postProductDto.TagIds.Distinct().Contains(t.Id)).ToListAsync();
            if(tags.Count!=postProductDto.TagIds.Count)
            {
                throw new Exception("tag doesnt exist");
            }

            Product product = _mapper.Map<Product>(postProductDto);
            _repository.Add(product);

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(long id, PutProductDto putProductDto)
        {
            bool result = await _repository.AnyAsync(p => p.Name == putProductDto.Name && p.Id != id);
            if (result)
            {
                throw new Exception("Product already exists");
            }

            bool categoryResult = await _categoryRepository.AnyAsync(c => c.Id == putProductDto.CategoryId);
            if (!categoryResult)
            {
                throw new Exception("category not found");
            }

            var tags = await _tagRepository.GetAll(tag => putProductDto.TagIds.Distinct().Contains(tag.Id)).ToListAsync();
            if (tags.Count != putProductDto.TagIds.Count)
            {
                throw new Exception("tag doesnt exist");
            }

            Product product = await _repository.GetByIdAsync(id,"ProductTags");


            _repository.Update(_mapper.Map(putProductDto,product));
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(long id)
        {
            Product? existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new Exception("category not found");
            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
        }



        public async Task SoftDeleteAsync(long id)
        {
            Product? existed = await _repository.GetByIdAsync(id);


            if (existed == null) throw new Exception("category not found");

            existed.IsDeleted = true;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }

    }
}
