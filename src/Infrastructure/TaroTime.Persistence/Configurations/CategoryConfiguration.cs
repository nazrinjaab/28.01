using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaroTime.Domain.Entities;


namespace TaroTime.Persistence.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
               .Property(c => c.Name)
               .IsRequired()
               .HasColumnType("varchar(150)");

            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            

        }
    }
}
