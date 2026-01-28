using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaroTime.Domain.Entities;



namespace TaroTime.Persistence.Configurations
{
    internal class ProductColorConfiguration: IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder
                .HasKey(pc => new { pc.ProductId, pc.ColorId });
        }
    }


}
