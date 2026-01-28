using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaroTime.Domain.Entities;

namespace TaroTime.Persistence.Configurations
{
    internal class AppUserConfiguration:IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
               .Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(64);

            builder
               .Property(u => u.Surname)
               .IsRequired()
               .HasMaxLength(64);
        }
    }
}
