using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities;

namespace TaroTime.Persistence.Configurations
{
    internal class CoffeeReadingConfiguration : IEntityTypeConfiguration<CoffeeReading>
    {
        public void Configure(EntityTypeBuilder<CoffeeReading> builder)
        {
            builder.HasKey(cr => cr.Id);

            
            builder.Property(cr => cr.UserId)
                .IsRequired();

            builder.Property(cr => cr.CoffeeReaderId)
                .IsRequired(false);

            builder.Property(cr => cr.Question)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(cr => cr.Answer)
                .HasMaxLength(2000);

            builder.Property(cr => cr.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(cr => cr.AcceptedAt)
                .IsRequired(false);

            builder.Property(cr => cr.CompletedAt)
                .IsRequired(false);

            builder.Property(cr => cr.CupImagePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(cr => cr.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

    }
}
