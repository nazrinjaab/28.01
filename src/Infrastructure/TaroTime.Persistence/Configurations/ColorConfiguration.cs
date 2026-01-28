using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaroTime.Domain.Entities;


namespace TaroTime.Persistence.Configurations
{
    internal class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder
              .Property(p => p.Name)
              .IsRequired()
              .HasColumnType("varchar(150)");

            builder
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}
