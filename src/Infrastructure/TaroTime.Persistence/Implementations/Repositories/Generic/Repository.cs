using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Domain.Entities.common;
using TaroTime.Persistence.Contexts;

namespace TaroTime.Persistence.Implementations.Repositories.Generic
{
    internal class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll(
            Expression<Func<T, bool>>? func = null,

             Expression<Func<T, object>>? sort = null,
             bool isDesc = false,
             bool isIgnore=false,
            int page = 0,
            int take = 0,
            params string[]? includes
            )
        {
            IQueryable<T> query = _dbSet;



            if (func != null)
                query = query.Where(func);




            if (sort != null)
            {
                if (isDesc)
                    query = query.OrderByDescending(sort);

                else query = query.OrderBy(sort);
            }

            if (page > 0 && take > 0)
            {
                query = query.Skip((page - 1) * take).Take(take);
            }

            if (includes != null)
            {
                query = _getIncludes(query, includes);
            }

            if (isIgnore)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }

        public async Task<T?> GetByIdAsync(long id, params string[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            if (includes != null)
            {
                query = _getIncludes(query, includes);
            }
            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected IQueryable<T> _getIncludes(IQueryable<T> query, params string[] includes)
        {
            for (int i = 0; i < includes.Length; i++)
            {
                query = query.Include(includes[i]);
            }
            return query;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> func)
        {
            return await _dbSet.AnyAsync(func);
        }
    }
}
