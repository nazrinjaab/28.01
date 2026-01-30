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
    internal class TarotReadingConfiguration : IEntityTypeConfiguration<TarotReading>
    {
        public void Configure(EntityTypeBuilder<TarotReading> builder)
        {
            builder.HasKey(tr => tr.Id);

            builder.HasOne(tr => tr.User)
                .WithMany()
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tr => tr.TarotReader)
                .WithMany()
                .HasForeignKey(tr => tr.TarotReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(tr => tr.Question)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(tr => tr.Answer)
                .HasMaxLength(2000);

            builder.Property(tr => tr.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(tr => tr.AcceptedAt)
                .IsRequired();

            builder.Property(tr => tr.CompletedAt)
                .IsRequired();

            builder.Property(tr => tr.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

    }
}
