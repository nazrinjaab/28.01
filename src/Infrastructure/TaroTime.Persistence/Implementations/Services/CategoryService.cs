using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaroTime.Application.DTOs;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;

namespace TaroTime.Persistence.Implementations.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<GetCategoryItemDto>> GetAllAsync(int page, int take)
        {
          var categories =  await _repository
                .GetAll(
              sort: c => c.Name,
              page: page,
              take: take,
              includes: nameof(Category.Products)
              ).ToListAsync();

            return _mapper.Map<IReadOnlyList<GetCategoryItemDto>>(categories);

          
        }

        public async Task<GetCategoryDto> GetByIdAsync(long id)
        {
            Category? category = await _repository.GetByIdAsync(id, nameof(Category.Products));

            if (category == null) throw new Exception("category not found");

            return _mapper.Map<GetCategoryDto>(category);
                
          
        }


        public async Task CreateAsync(PostCategoryDto categoryDto)
        {
            bool result = await _repository.AnyAsync(c=>c.Name==categoryDto.Name);
            if (result)
            {
                throw new Exception("Category already exists");
            }

            Category category= _mapper.Map<Category>(categoryDto);

            _repository.Add(category);
            await _repository.SaveChangesAsync();
        }



        public async Task UpdateAsync(long id, PutCategoryDto categoryDto)
        {
            bool result = await _repository.AnyAsync(c => c.Name == categoryDto.Name && c.Id!=id);
            if (result)
            {
                throw new Exception("Category already exists");
            }


            Category? existed = await _repository.GetByIdAsync(id);


            if (existed == null) throw new Exception("category not found");


            existed = _mapper.Map(categoryDto,existed);

           
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }


        public async Task DeleteAsync(long id)
        {
            Category? existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new Exception("category not found");
            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
        }



        public async Task SoftDeleteAsync(long id)
        {
            Category? existed = await _repository.GetByIdAsync(id);


            if (existed == null) throw new Exception("category not found");

            existed.IsDeleted = true;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }
    }
}
