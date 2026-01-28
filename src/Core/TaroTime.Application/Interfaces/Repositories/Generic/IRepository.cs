
using System.Linq.Expressions;
using TaroTime.Domain.Entities.common;

namespace TaroTime.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(
        Expression<Func<T, bool>>? func = null,
        Expression<Func<T, object>>? sort = null,
        bool isDesc = false,
        bool isIgnore = false,
        int page = 0,
        int take = 0,
        params string[]? includes
        );
        Task<T> GetByIdAsync(long id, params string[] includes);
        //Task<T> GetByIdStringAsync(string id, params string[] includes);
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);


        Task<bool> AnyAsync(Expression<Func<T, bool>> func);


        Task SaveChangesAsync();

    }
}
