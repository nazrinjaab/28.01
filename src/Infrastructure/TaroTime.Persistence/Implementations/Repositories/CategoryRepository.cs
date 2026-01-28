using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Domain.Entities;
using TaroTime.Persistence.Contexts;
using TaroTime.Persistence.Implementations.Repositories.Generic;

namespace TaroTime.Persistence.Implementations.Repositories
{
    internal class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
       
    }
}
