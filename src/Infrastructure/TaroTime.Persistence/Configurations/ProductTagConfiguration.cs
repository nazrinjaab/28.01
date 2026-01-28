using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaroTime.Domain.Entities;


namespace TaroTime.Persistence.Configurations
{
    internal class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder
                .HasKey(pt => new {pt.ProductId,pt.TagId });
        }
    }
}
