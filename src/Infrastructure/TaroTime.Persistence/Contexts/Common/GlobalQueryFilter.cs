using Microsoft.EntityFrameworkCore;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Entities.common;

namespace TaroTime.Persistence.Contexts.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyAllQueryFilters(this ModelBuilder builder)
        {
            builder.ApplyQueryFilter<Category>();
            builder.ApplyQueryFilter<Product>();
            builder.ApplyQueryFilter<Color>();
            builder.ApplyQueryFilter<Tag>();
        }
        public static void ApplyQueryFilter<T>(this ModelBuilder builder) where T : BaseEntity, new()
        {
            builder.Entity<T>().HasQueryFilter(x=>x.IsDeleted==false);
        }
    }
}
