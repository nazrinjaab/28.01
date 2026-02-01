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
    internal class PalmReadingConfiguration : IEntityTypeConfiguration<PalmReading>
    {
        public void Configure(EntityTypeBuilder<PalmReading> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.UserId)
                .IsRequired();

            builder.Property(pr => pr.PalmReaderId)
                .IsRequired(false);

            builder.Property(pr => pr.Question)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(pr => pr.Answer)
                .HasMaxLength(2000);

            builder.Property(pr => pr.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(pr => pr.AcceptedAt)
                .IsRequired(false);

            builder.Property(pr => pr.CompletedAt)
                .IsRequired(false);

            builder.Property(pr => pr.HandImagePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.Res)
                .IsRequired()
                .HasDefaultValue(string.Empty);

            builder.Property(pr => pr.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }

}
