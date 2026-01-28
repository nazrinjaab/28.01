

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaroTime.Domain.Entities;

namespace TaroTime.Persistence.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
               .Property(p => p.Name)
               .IsRequired()
               .HasColumnType("varchar(150)");

            builder
                .HasIndex(p => p.Name)
                .IsUnique();

            builder
                 .Property(p => p.Price)
                 .HasColumnType("decimal(8,2)");

            builder
                .Property(p => p.SKU)
                .HasColumnType("char(10)");

            builder
                .HasIndex(p => p.SKU)
                .IsUnique();






        }
    }
}
